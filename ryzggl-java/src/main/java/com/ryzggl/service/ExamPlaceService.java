package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.ExamPlace;
import com.ryzggl.repository.ExamPlaceRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * ExamPlace Service
 * 考点服务层
 *
 * Business logic for exam place/venue management
 */
@Service
public class ExamPlaceService {

    private static final Logger logger = LoggerFactory.getLogger(ExamPlaceService.class);

    @Autowired
    private ExamPlaceRepository examPlaceRepository;

    /**
     * Create ExamPlace
     * 创建考点
     *
     * @param examPlace ExamPlace entity
     * @return Created ExamPlace
     */
    @Transactional
    public ExamPlace createPlace(ExamPlace examPlace) {
        try {
            // Validate required fields
            validateExamPlace(examPlace);

            // Set default status if not provided
            if (examPlace.getStatus() == null || examPlace.getStatus().isEmpty()) {
                examPlace.setStatus("ACTIVE");
            }

            examPlaceRepository.insert(examPlace);
            logger.info("ExamPlace created successfully: ExamPlaceID={}", examPlace.getExamPlaceId());

            return examPlace;
        } catch (Exception e) {
            logger.error("Error creating ExamPlace", e);
            throw new RuntimeException("创建考点失败: " + e.getMessage(), e);
        }
    }

    /**
     * Update ExamPlace
     * 更新考点
     *
     * @param examPlace ExamPlace entity
     * @return Updated ExamPlace
     */
    @Transactional
    public ExamPlace updatePlace(ExamPlace examPlace) {
        try {
            if (examPlace.getExamPlaceId() == null) {
                throw new IllegalArgumentException("考点ID不能为空");
            }

            // Check if record exists
            ExamPlace existing = examPlaceRepository.selectById(examPlace.getExamPlaceId());
            if (existing == null) {
                throw new IllegalArgumentException("考点不存在");
            }

            // Validate required fields
            validateExamPlace(examPlace);

            examPlaceRepository.updateById(examPlace);
            logger.info("ExamPlace updated successfully: ExamPlaceID={}", examPlace.getExamPlaceId());

            return examPlace;
        } catch (Exception e) {
            logger.error("Error updating ExamPlace: ExamPlaceID={}", examPlace.getExamPlaceId(), e);
            throw new RuntimeException("更新考点失败: " + e.getMessage(), e);
        }
    }

    /**
     * Delete ExamPlace
     * 删除考点
     *
     * @param examPlaceId Exam Place ID
     * @return true if deleted successfully
     */
    @Transactional
    public boolean deletePlace(Long examPlaceId) {
        try {
            if (examPlaceId == null) {
                throw new IllegalArgumentException("考点ID不能为空");
            }

            int result = examPlaceRepository.deleteById(examPlaceId);
            boolean deleted = result > 0;

            if (deleted) {
                logger.info("ExamPlace deleted successfully: ExamPlaceID={}", examPlaceId);
            } else {
                logger.warn("ExamPlace not found for deletion: ExamPlaceID={}", examPlaceId);
            }

            return deleted;
        } catch (Exception e) {
            logger.error("Error deleting ExamPlace: ExamPlaceID={}", examPlaceId, e);
            throw new RuntimeException("删除考点失败: " + e.getMessage(), e);
        }
    }

    /**
     * Get ExamPlace by ID
     * 根据ID获取考点
     *
     * @param examPlaceId Exam Place ID
     * @return ExamPlace entity or null
     */
    public ExamPlace getPlaceById(Long examPlaceId) {
        try {
            return examPlaceRepository.selectById(examPlaceId);
        } catch (Exception e) {
            logger.error("Error getting ExamPlace: ExamPlaceID={}", examPlaceId, e);
            throw new RuntimeException("获取考点失败: " + e.getMessage(), e);
        }
    }

    /**
     * Get ExamPlace by Name
     * 根据名称获取考点
     *
     * @param examPlaceName Exam Place name
     * @return ExamPlace entity or null
     */
    public ExamPlace getPlaceByName(String examPlaceName) {
        try {
            return examPlaceRepository.getByExamPlaceName(examPlaceName);
        } catch (Exception e) {
            logger.error("Error getting ExamPlace by name: examPlaceName={}", examPlaceName, e);
            throw new RuntimeException("获取考点失败: " + e.getMessage(), e);
        }
    }

    /**
     * Get ExamPlace list with pagination
     * 获取考点列表（分页）
     *
     * @param current Current page number (default: 1)
     * @param size Page size (default: 10)
     * @param status Filter by status (optional)
     * @return Page of ExamPlace entities
     */
    public IPage<ExamPlace> getPlaceList(int current, int size, String status) {
        try {
            Page<ExamPlace> page = new Page<>(current, size);

            if (status != null && !status.isEmpty()) {
                return examPlaceRepository.getByStatus(page, status);
            } else {
                return examPlaceRepository.selectPage(page, null);
            }
        } catch (Exception e) {
            logger.error("Error getting ExamPlace list", e);
            throw new RuntimeException("获取考点列表失败: " + e.getMessage(), e);
        }
    }

    /**
     * Search ExamPlace by name
     * 搜索考点（按名称）
     *
     * @param current Current page number
     * @param size Page size
     * @param examPlaceName Exam Place name (partial match)
     * @param status Status filter (optional)
     * @return Page of ExamPlace entities
     */
    public IPage<ExamPlace> searchPlaces(int current, int size,
                                        String examPlaceName,
                                        String status) {
        try {
            Page<ExamPlace> page = new Page<>(current, size);
            return examPlaceRepository.search(page, examPlaceName, status);
        } catch (Exception e) {
            logger.error("Error searching ExamPlace", e);
            throw new RuntimeException("搜索考点失败: " + e.getMessage(), e);
        }
    }

    /**
     * Get active exam places
     * 获取可用的考点列表
     *
     * @param current Current page number
     * @param size Page size
     * @return Page of ExamPlace entities
     */
    public IPage<ExamPlace> getActivePlaces(int current, int size) {
        try {
            Page<ExamPlace> page = new Page<>(current, size);
            return examPlaceRepository.getActivePlaces(page);
        } catch (Exception e) {
            logger.error("Error getting active ExamPlace", e);
            throw new RuntimeException("获取可用考点列表失败: " + e.getMessage(), e);
        }
    }

    /**
     * Get places with available capacity
     * 获取有容量的考点列表
     *
     * @param current Current page number
     * @param size Page size
     * @param requiredCapacity Required minimum capacity
     * @param status Status filter (optional)
     * @return Page of ExamPlace entities
     */
    public IPage<ExamPlace> getPlacesWithCapacity(int current, int size,
                                                    Integer requiredCapacity,
                                                    String status) {
        try {
            Page<ExamPlace> page = new Page<>(current, size);
            return examPlaceRepository.getPlacesWithCapacity(page, requiredCapacity, status);
        } catch (Exception e) {
            logger.error("Error getting ExamPlace with capacity: requiredCapacity={}", requiredCapacity, e);
            throw new RuntimeException("获取考点列表失败: " + e.getMessage(), e);
        }
    }

    /**
     * Count ExamPlace by status
     * 根据状态统计考点数量
     *
     * @param status Exam Place status
     * @return Count
     */
    public long countByStatus(String status) {
        try {
            if (status == null || status.isEmpty()) {
                return examPlaceRepository.selectCount(null);
            }
            return examPlaceRepository.countByStatus(status);
        } catch (Exception e) {
            logger.error("Error counting ExamPlace: status={}", status, e);
            throw new RuntimeException("统计考点数量失败: " + e.getMessage(), e);
        }
    }

    /**
     * Update ExamPlace status
     * 更新考点状态
     *
     * @param examPlaceId Exam Place ID
     * @param status New status
     * @return Updated ExamPlace
     */
    @Transactional
    public ExamPlace updateStatus(Long examPlaceId, String status) {
        try {
            ExamPlace existing = getPlaceById(examPlaceId);
            if (existing == null) {
                throw new IllegalArgumentException("考点不存在");
            }

            existing.setStatus(status);
            examPlaceRepository.updateById(existing);

            logger.info("ExamPlace status updated: ExamPlaceID={}, status={}", examPlaceId, status);
            return existing;
        } catch (Exception e) {
            logger.error("Error updating ExamPlace status: ExamPlaceID={}, status={}", examPlaceId, status, e);
            throw new RuntimeException("更新考点状态失败: " + e.getMessage(), e);
        }
    }

    /**
     * Validate exam place before creation/update
     * 验证考点数据
     *
     * @param examPlace ExamPlace entity
     */
    private void validateExamPlace(ExamPlace examPlace) {
        if (examPlace.getExamPlaceName() == null || examPlace.getExamPlaceName().isEmpty()) {
            throw new IllegalArgumentException("考点名称不能为空");
        }

        if (examPlace.getExamPlaceAddress() == null || examPlace.getExamPlaceAddress().isEmpty()) {
            throw new IllegalArgumentException("考点地址不能为空");
        }

        if (examPlace.getLinkMan() == null || examPlace.getLinkMan().isEmpty()) {
            throw new IllegalArgumentException("联系人不能为空");
        }

        if (examPlace.getPhone() == null || examPlace.getPhone().isEmpty()) {
            throw new IllegalArgumentException("联系电话不能为空");
        }

        // Validate phone format (Chinese phone number)
        String phone = examPlace.getPhone();
        if (!phone.matches("^1[3-9]\\d{9}$")) {
            throw new IllegalArgumentException("联系电话格式不正确");
        }

        // Validate capacity values
        if (examPlace.getRoomNum() != null && examPlace.getRoomNum() < 0) {
            throw new IllegalArgumentException("房间数量不能为负数");
        }

        if (examPlace.getExamPersonNum() != null && examPlace.getExamPersonNum() < 0) {
            throw new IllegalArgumentException("考试人数不能为负数");
        }

        // Validate status value
        if (examPlace.getStatus() != null && !examPlace.getStatus().isEmpty()) {
            String status = examPlace.getStatus();
            if (!status.equals("ACTIVE") && !status.equals("INACTIVE") &&
                    !status.equals("DELETED") && !status.equals("已删除")) {
                throw new IllegalArgumentException("状态值不正确，应为: ACTIVE, INACTIVE, DELETED");
            }
        }
    }
}
