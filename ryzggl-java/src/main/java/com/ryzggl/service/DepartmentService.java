package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Department;
import com.ryzggl.repository.DepartmentRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

/**
 * Department Service - 部门管理
 * Department management service
 */
@Service
public class DepartmentService extends ServiceImpl<DepartmentRepository, Department> {

    private static final Logger logger = LoggerFactory.getLogger(DepartmentService.class);

    private final DepartmentRepository departmentRepository;

    public DepartmentService(DepartmentRepository departmentRepository) {
        this.departmentRepository = departmentRepository;
    }

    /**
     * Query department list with pagination
     */
    public Result<IPage<Department>> getDepartmentList(Integer current, Integer size,
                                                      String departmentName, String departmentType) {
        Page<Department> page = new Page<>(current, size);
        IPage<Department> result = departmentRepository.selectDepartmentPage(page, departmentName, departmentType);
        return Result.success(result);
    }

    /**
     * Query all departments (for dropdown)
     */
    public Result<java.util.List<Department>> getAllDepartments() {
        java.util.List<Department> departments = departmentRepository.selectAll();
        return Result.success(departments);
    }

    /**
     * Query department by ID
     */
    public Result<Department> getDepartmentById(String id) {
        Department department = departmentRepository.selectById(id);
        if (department == null) {
            return Result.error("部门不存在");
        }
        return Result.success(department);
    }

    /**
     * Query departments by parent ID (for tree structure)
     */
    public Result<java.util.List<Department>> getDepartmentsByParentId(String parentId) {
        java.util.List<Department> departments = departmentRepository.selectByParentId(parentId);
        return Result.success(departments);
    }

    /**
     * Query top-level departments
     */
    public Result<java.util.List<Department>> getTopLevelDepartments() {
        java.util.List<Department> departments = departmentRepository.selectTopLevel();
        return Result.success(departments);
    }

    /**
     * Create department
     */
    @Transactional
    public Result<Department> createDepartment(Department department) {
        // Validate parent department exists if parentId is provided
        if (department.getParentId() != null && !department.getParentId().isEmpty()) {
            Department parent = departmentRepository.selectById(department.getParentId());
            if (parent == null) {
                return Result.error("上级部门不存在");
            }
        }

        department.setCreateBy("system");
        department.setUpdateBy("system");

        save(department);
        logger.info("Created department: DepartmentID={}", department.getDepartmentId());
        return Result.success("部门创建成功", department);
    }

    /**
     * Update department
     */
    @Transactional
    public Result<Department> updateDepartment(Department department) {
        Department exist = departmentRepository.selectById(department.getDepartmentId());
        if (exist == null) {
            return Result.error("部门不存在");
        }

        // Prevent setting parent to self
        if (department.getDepartmentId().equals(department.getParentId())) {
            return Result.error("不能将部门设置为自己的上级部门");
        }

        // Validate parent department exists if parentId is changed
        if (department.getParentId() != null && !department.getParentId().isEmpty()
                && !department.getParentId().equals(exist.getParentId())) {
            Department parent = departmentRepository.selectById(department.getParentId());
            if (parent == null) {
                return Result.error("上级部门不存在");
            }

            // Prevent circular reference
            if (hasCircularReference(department.getDepartmentId(), department.getParentId())) {
                return Result.error("不能形成循环引用");
            }
        }

        department.setUpdateBy("system");

        updateById(department);
        logger.info("Updated department: DepartmentID={}", department.getDepartmentId());
        return Result.success("部门更新成功", department);
    }

    /**
     * Delete department
     */
    @Transactional
    public Result<Boolean> deleteDepartment(String id) {
        Department department = departmentRepository.selectById(id);
        if (department == null) {
            return Result.error("部门不存在");
        }

        // Check if department has children
        int childCount = departmentRepository.countByParentId(id);
        if (childCount > 0) {
            return Result.error("该部门下还有子部门，无法删除");
        }

        boolean deleted = removeById(id);
        if (deleted) {
            logger.info("Deleted department: DepartmentID={}", id);
        }
        return Result.success(deleted);
    }

    /**
     * Check for circular reference in department hierarchy
     */
    private boolean hasCircularReference(String departmentId, String newParentId) {
        String currentParentId = newParentId;
        int maxDepth = 100; // Prevent infinite loop
        int depth = 0;

        while (currentParentId != null && !currentParentId.isEmpty() && depth < maxDepth) {
            if (currentParentId.equals(departmentId)) {
                return true; // Circular reference found
            }

            Department parent = departmentRepository.selectById(currentParentId);
            if (parent == null) {
                break;
            }

            currentParentId = parent.getParentId();
            depth++;
        }

        return false;
    }
}
