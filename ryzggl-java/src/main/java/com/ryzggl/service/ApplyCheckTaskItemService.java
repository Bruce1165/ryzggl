package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ApplyCheckTask;
import com.ryzggl.entity.ApplyCheckTaskItem;
import com.ryzggl.repository.ApplyCheckTaskItemRepository;
import com.ryzggl.repository.ApplyCheckTaskRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Date;
import java.util.List;

/**
 * ApplyCheckTaskItem Service
 * Individual item management for check tasks
 */
@Service
public class ApplyCheckTaskItemService extends ServiceImpl<ApplyCheckTaskItemRepository, ApplyCheckTaskItem> {

    private static final Logger logger = LoggerFactory.getLogger(ApplyCheckTaskItemService.class);

    @Autowired
    private ApplyCheckTaskItemRepository applyCheckTaskItemRepository;

    @Autowired
    private ApplyCheckTaskRepository applyCheckTaskRepository;

    /**
     * Create a new check task item
     *
     * @param item Check task item to create
     * @return Created item
     */
    @Transactional
    public ApplyCheckTaskItem createItem(ApplyCheckTaskItem item) {
        applyCheckTaskItemRepository.insert(item);
        logger.info("Created check task item: TaskItemID={}, TaskID={}",
                item.getTaskItemID(), item.getTaskID());
        return item;
    }

    /**
     * Update check task item
     *
     * @param item Check task item to update
     * @return Updated item
     */
    @Transactional
    public ApplyCheckTaskItem updateItem(ApplyCheckTaskItem item) {
        applyCheckTaskItemRepository.updateById(item);
        logger.info("Updated check task item: TaskItemID={}", item.getTaskItemID());
        return item;
    }

    /**
     * Delete check task item
     *
     * @param taskItemId Task Item ID
     */
    @Transactional
    public boolean deleteItem(Long taskItemId) {
        boolean deleted = removeById(taskItemId);
        logger.info("Deleted check task item: TaskItemID={}, success={}", taskItemId, deleted);
        return deleted;
    }

    /**
     * Delete all items by Task ID
     *
     * @param taskId Task ID
     */
    @Transactional
    public int deleteByTaskId(Long taskId) {
        int deleted = applyCheckTaskItemRepository.deleteByTaskId(taskId);
        logger.info("Deleted {} items for task: TaskID={}", deleted, taskId);
        return deleted;
    }

    /**
     * Get check task item by ID
     *
     * @param taskItemId Task Item ID
     * @return Check task item
     */
    public ApplyCheckTaskItem getItemById(Long taskItemId) {
        return applyCheckTaskItemRepository.selectById(taskItemId);
    }

    /**
     * Get all items for a task
     *
     * @param taskId Task ID
     * @return List of items
     */
    public List<ApplyCheckTaskItem> getItemsByTaskId(Long taskId) {
        QueryWrapper<ApplyCheckTaskItem> wrapper = new QueryWrapper<>();
        wrapper.eq("TaskID", taskId);
        wrapper.orderByAsc("TaskItemID");
        return applyCheckTaskItemRepository.selectList(wrapper);
    }

    /**
     * Get unchecked items for a task
     *
     * @param taskId Task ID
     * @return List of unchecked items
     */
    public List<ApplyCheckTaskItem> getUncheckedItems(Long taskId) {
        return applyCheckTaskItemRepository.selectUncheckedItems(taskId);
    }

    /**
     * Get checked items for a task
     *
     * @param taskId Task ID
     * @return List of checked items
     */
    public List<ApplyCheckTaskItem> getCheckedItems(Long taskId) {
        return applyCheckTaskItemRepository.selectCheckedItems(taskId);
    }

    /**
     * Approve a single check item
     *
     * @param taskItemId Task Item ID
     * @param checkMan Approver name
     * @param checkResult Check result
     * @param checkResultDesc Check result description
     * @return Updated item
     */
    @Transactional
    public ApplyCheckTaskItem approveItem(Long taskItemId, String checkMan,
                                         String checkResult, String checkResultDesc) {
        ApplyCheckTaskItem item = getItemById(taskItemId);
        if (item != null) {
            item.setCheckMan(checkMan);
            item.setCheckTime(new Date());
            item.setCheckResult(checkResult);
            item.setCheckResultDesc(checkResultDesc);
            updateItem(item);
            logger.info("Approved item: TaskItemID={}, CheckMan={}, CheckResult={}",
                    taskItemId, checkMan, checkResult);
        }
        return item;
    }

    /**
     * Batch update check results
     *
     * @param checkMan Approver name
     * @param checkResult Check result
     * @param checkResultDesc Check result description
     * @param filterWhere Where clause condition
     * @return Number of affected rows
     */
    @Transactional
    public int batchUpdateCheck(String checkMan, String checkResult,
                               String checkResultDesc, String filterWhere) {
        int affected = applyCheckTaskItemRepository.batchUpdateCheck(
                checkMan, checkResult, checkResultDesc, filterWhere);
        logger.info("Batch updated {} items: CheckMan={}, CheckResult={}",
                affected, checkMan, checkResult);
        return affected;
    }

    /**
     * Re-check an item (second-level approval)
     *
     * @param taskItemId Task Item ID
     * @param reCheckMan Re-checker name
     * @return Updated item
     */
    @Transactional
    public ApplyCheckTaskItem reCheckItem(Long taskItemId, String reCheckMan) {
        ApplyCheckTaskItem item = getItemById(taskItemId);
        if (item != null) {
            item.setReCheckMan(reCheckMan);
            item.setReCheckTime(new Date());
            updateItem(item);
            logger.info("Re-checked item: TaskItemID={}, ReCheckMan={}", taskItemId, reCheckMan);
        }
        return item;
    }

    // ==================== SAMPLING METHODS ====================

    /**
     * Sample and insert items based on business type
     * Fully implements legacy sampling logic from ApplyCheckTaskItemDAL.cs
     *
     * Business Types:
     * - 1 (二建): Sample from Apply table
     * - 2 (二造): Sample from zjs_Apply table
     * - 3 (安管人员): Sample from 3 sources - ExamSignUp, VIEW_CERTIFICATE_ENTER, CertificateContinue
     * - 4 (特种作业): Sample from 2 sources - ExamSignUp, CertificateContinue
     *
     * @param taskId Task ID
     * @param startDate Start date
     * @param endDate End date
     * @param checkPer Check percentage (x/1000, e.g., 10 = 1%)
     * @param busTypeID Business Type ID
     * @return Number of items inserted
     */
    @Transactional
    public int sampleAndInsertItems(Long taskId, String startDate, String endDate,
                                   Integer checkPer, Integer busTypeID) {
        logger.info("Starting sampling: TaskID={}, StartDate={}, EndDate={}, CheckPer={}%, BusTypeID={}",
                taskId, startDate, endDate, checkPer, busTypeID);

        int totalItems = 0;

        switch (busTypeID) {
            case 1: // 二建
                totalItems = sampleFromApply(taskId, startDate, endDate, checkPer);
                break;
            case 2: // 二造
                totalItems = sampleFromZjsApply(taskId, startDate, endDate, checkPer);
                break;
            case 3: // 安管人员
                totalItems = sampleFromSafetyPersonnel(taskId, startDate, endDate, checkPer);
                break;
            case 4: // 特种作业
                totalItems = sampleFromSpecialWork(taskId, startDate, endDate, checkPer);
                break;
            default:
                logger.warn("Unknown business type ID: {}", busTypeID);
                return 0;
        }

        logger.info("Sampling complete: TaskID={}, BusTypeID={}, Total items sampled={}", taskId, busTypeID, totalItems);
        return totalItems;
    }

    /**
     * Sample from Apply table (二建)
     */
    private int sampleFromApply(Long taskId, String startDate, String endDate, Integer checkPer) {
        int totalCount = applyCheckTaskItemRepository.countFromApply(startDate, endDate);
        int sampleSize = calculateSampleLimit(totalCount, checkPer);

        if (sampleSize == 0) {
            return 0;
        }

        List<ApplyCheckTaskItem> samples = applyCheckTaskItemRepository.sampleFromApply(
                taskId, startDate, endDate, sampleSize);
        int inserted = 0;
        for (ApplyCheckTaskItem item : samples) {
            createItem(item);
            inserted++;
        }

        logger.info("Sampled from Apply table: TaskID={}, Total={}, Sampled={}, Inserted={}",
                taskId, totalCount, sampleSize, inserted);
        return inserted;
    }

    /**
     * Sample from zjs_Apply table (二造)
     */
    private int sampleFromZjsApply(Long taskId, String startDate, String endDate, Integer checkPer) {
        int totalCount = applyCheckTaskItemRepository.countFromZjsApply(startDate, endDate);
        int sampleSize = calculateSampleLimit(totalCount, checkPer);

        if (sampleSize == 0) {
            return 0;
        }

        List<ApplyCheckTaskItem> samples = applyCheckTaskItemRepository.sampleFromZjsApply(
                taskId, startDate, endDate, sampleSize);
        int inserted = 0;
        for (ApplyCheckTaskItem item : samples) {
            createItem(item);
            inserted++;
        }

        logger.info("Sampled from zjs_Apply table: TaskID={}, Total={}, Sampled={}, Inserted={}",
                taskId, totalCount, sampleSize, inserted);
        return inserted;
    }

    /**
     * Sample for safety personnel (安管人员)
     * Samples from 3 sources: ExamSignUp, VIEW_CERTIFICATE_ENTER, CertificateContinue
     */
    private int sampleFromSafetyPersonnel(Long taskId, String startDate, String endDate, Integer checkPer) {
        int inserted = 0;

        // 1. ExamSignUp (考试报名)
        int totalCount1 = applyCheckTaskItemRepository.countFromExamSignUpForSafety(startDate, endDate);
        int limit1 = calculateSampleLimit(totalCount1, checkPer);
        if (limit1 > 0) {
            List<ApplyCheckTaskItem> samples1 = applyCheckTaskItemRepository.sampleFromExamSignUpForSafety(
                    taskId, startDate, endDate, limit1);
            for (ApplyCheckTaskItem item : samples1) {
                createItem(item);
                inserted++;
            }
        }

        // 2. VIEW_CERTIFICATE_ENTER (证书进京)
        int totalCount2 = applyCheckTaskItemRepository.countFromCertificateEnter(startDate, endDate);
        int limit2 = calculateSampleLimit(totalCount2, checkPer);
        if (limit2 > 0) {
            List<ApplyCheckTaskItem> samples2 = applyCheckTaskItemRepository.sampleFromCertificateEnter(
                    taskId, startDate, endDate, limit2);
            for (ApplyCheckTaskItem item : samples2) {
                createItem(item);
                inserted++;
            }
        }

        // 3. CertificateContinue (证书续期)
        int totalCount3 = applyCheckTaskItemRepository.countFromCertificateContinueForSafety(startDate, endDate);
        int limit3 = calculateSampleLimit(totalCount3, checkPer);
        if (limit3 > 0) {
            List<ApplyCheckTaskItem> samples3 = applyCheckTaskItemRepository.sampleFromCertificateContinueForSafety(
                    taskId, startDate, endDate, limit3);
            for (ApplyCheckTaskItem item : samples3) {
                createItem(item);
                inserted++;
            }
        }

        logger.info("Sampled for safety personnel: TaskID={}, Total={}, Inserted={}",
                taskId, totalCount1 + totalCount2 + totalCount3, inserted);
        return inserted;
    }

    /**
     * Sample for special work (特种作业)
     * Samples from 2 sources: ExamSignUp, CertificateContinue
     */
    private int sampleFromSpecialWork(Long taskId, String startDate, String endDate, Integer checkPer) {
        int inserted = 0;

        // 1. ExamSignUp (考试报名)
        int totalCount1 = applyCheckTaskItemRepository.countFromExamSignUpForSpecial(startDate, endDate);
        int limit1 = calculateSampleLimit(totalCount1, checkPer);
        if (limit1 > 0) {
            List<ApplyCheckTaskItem> samples1 = applyCheckTaskItemRepository.sampleFromExamSignUpForSpecial(
                    taskId, startDate, endDate, limit1);
            for (ApplyCheckTaskItem item : samples1) {
                createItem(item);
                inserted++;
            }
        }

        // 2. CertificateContinue (证书续期)
        int totalCount2 = applyCheckTaskItemRepository.countFromCertificateContinueForSpecial(startDate, endDate);
        int limit2 = calculateSampleLimit(totalCount2, checkPer);
        if (limit2 > 0) {
            List<ApplyCheckTaskItem> samples2 = applyCheckTaskItemRepository.sampleFromCertificateContinueForSpecial(
                    taskId, startDate, endDate, limit2);
            for (ApplyCheckTaskItem item : samples2) {
                createItem(item);
                inserted++;
            }
        }

        logger.info("Sampled for special work: TaskID={}, Total={}, Inserted={}",
                taskId, totalCount1 + totalCount2, inserted);
        return inserted;
    }

    /**
     * Calculate sample limit based on percentage
     * Ensures minimum of 1 record is always sampled
     */
    private int calculateSampleLimit(int totalCount, Integer checkPer) {
        int limit = totalCount * checkPer / 1000;
        return Math.max(1, limit);
    }
}
