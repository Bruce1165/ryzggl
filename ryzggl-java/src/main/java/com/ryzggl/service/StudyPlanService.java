package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.StudyPlan;
import com.ryzggl.repository.StudyPlanRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

/**
 * StudyPlan Service
 * 学习计划业务逻辑层
 */
@Service
public class StudyPlanService implements IService<StudyPlan> {

    @Autowired
    private StudyPlanRepository studyPlanRepository;

    /**
     * Get study plan by worker certificate code and package ID
     */
    public StudyPlan getByWorkerCertificateAndPackage(String workerCertificateCode, Long packageId) {
        return studyPlanRepository.getByWorkerCertificateAndPackage(workerCertificateCode, packageId);
    }

    /**
     * Get study plans by worker certificate code
     */
    public List<StudyPlan> getByWorkerCertificateCode(String workerCertificateCode) {
        return studyPlanRepository.getByWorkerCertificateCode(workerCertificateCode);
    }

    /**
     * Get study plans by package ID
     */
    public List<StudyPlan> getByPackageId(Long packageId) {
        return studyPlanRepository.getByPackageId(packageId);
    }

    /**
     * Search study plans
     */
    public List<StudyPlan> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return studyPlanRepository.selectList(null);
        }
        return studyPlanRepository.search(keyword);
    }

    /**
     * Create study plan
     */
    @Transactional
    public Result<StudyPlan> createStudyPlan(StudyPlan studyPlan) {
        // Validate required fields
        if (studyPlan.getWorkerCertificateCode() == null || studyPlan.getWorkerCertificateCode().trim().isEmpty()) {
            return Result.error("人员证书编码不能为空");
        }

        if (studyPlan.getWorkerName() == null || studyPlan.getWorkerName().trim().isEmpty()) {
            return Result.error("人员姓名不能为空");
        }

        // Set default values
        if (studyPlan.getAddType() == null) {
            studyPlan.setAddType("个人添加");
        }
        if (studyPlan.getCreateTime() == null) {
            studyPlan.setCreateTime(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()));
        }
        if (studyPlan.getTestStatus() == null) {
            studyPlan.setTestStatus("未达标");
        }

        studyPlanRepository.insertStudyPlan(studyPlan);
        return Result.success(studyPlan);
    }

    /**
     * Update study plan
     */
    @Transactional
    public Result<StudyPlan> updateStudyPlan(StudyPlan studyPlan) {
        // Validate required fields
        if (studyPlan.getWorkerCertificateCode() == null || studyPlan.getPackageId() == null) {
            return Result.error("人员证书编码和培训包ID不能为空");
        }

        StudyPlan existing = getByWorkerCertificateAndPackage(
            studyPlan.getWorkerCertificateCode(),
            studyPlan.getPackageId()
        );
        if (existing == null) {
            return Result.error("学习计划不存在");
        }

        studyPlanRepository.updateStudyPlan(studyPlan);
        return Result.success(studyPlan);
    }

    /**
     * Update study plan status
     */
    @Transactional
    public Result<StudyPlan> updateStudyPlanStatus(String workerCertificateCode, Long packageId, String testStatus) {
        StudyPlan existing = getByWorkerCertificateAndPackage(workerCertificateCode, packageId);
        if (existing == null) {
            return Result.error("学习计划不存在");
        }

        existing.setTestStatus(testStatus);
        studyPlanRepository.updateStudyPlan(existing);
        return Result.success(existing);
    }

    /**
     * Delete study plan
     */
    @Transactional
    public Result<Void> deleteStudyPlan(String workerCertificateCode, Long packageId) {
        StudyPlan existing = getByWorkerCertificateAndPackage(workerCertificateCode, packageId);
        if (existing == null) {
            return Result.error("学习计划不存在");
        }

        studyPlanRepository.deleteStudyPlan(workerCertificateCode, packageId);
        return Result.success();
    }
}
