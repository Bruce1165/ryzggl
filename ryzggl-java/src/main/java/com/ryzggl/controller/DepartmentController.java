package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Department;
import com.ryzggl.service.DepartmentService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Department Controller
 * Department management endpoints
 */
@Tag(name = "部门管理", description = "部门管理接口")
@RestController
@RequestMapping("/api/v1/departments")
public class DepartmentController {

    private final DepartmentService departmentService;

    public DepartmentController(DepartmentService departmentService) {
        this.departmentService = departmentService;
    }

    public static class DepartmentQuery {
        private Integer current = 1;
        private Integer size = 10;
        private String departmentName;
        private String departmentType;

        public Integer getCurrent() {
            return current;
        }

        public void setCurrent(Integer current) {
            this.current = current;
        }

        public Integer getSize() {
            return size;
        }

        public void setSize(Integer size) {
            this.size = size;
        }

        public String getDepartmentName() {
            return departmentName;
        }

        public void setDepartmentName(String departmentName) {
            this.departmentName = departmentName;
        }

        public String getDepartmentType() {
            return departmentType;
        }

        public void setDepartmentType(String departmentType) {
            this.departmentType = departmentType;
        }
    }

    /**
     * Query department list
     */
    @Operation(summary = "查询部门列表")
    @GetMapping
    public Result<IPage<Department>> getDepartmentList(DepartmentQuery query) {
        return departmentService.getDepartmentList(query.getCurrent(), query.getSize(),
                query.getDepartmentName(), query.getDepartmentType());
    }

    /**
     * Query all departments (for dropdown)
     */
    @Operation(summary = "查询所有部门")
    @GetMapping("/all")
    public Result<List<Department>> getAllDepartments() {
        return departmentService.getAllDepartments();
    }

    /**
     * Query top-level departments
     */
    @Operation(summary = "查询顶级部门")
    @GetMapping("/top-level")
    public Result<List<Department>> getTopLevelDepartments() {
        return departmentService.getTopLevelDepartments();
    }

    /**
     * Query departments by parent ID
     */
    @Operation(summary = "根据父ID查询部门")
    @GetMapping("/parent/{parentId}")
    public Result<List<Department>> getDepartmentsByParentId(@PathVariable String parentId) {
        return departmentService.getDepartmentsByParentId(parentId);
    }

    /**
     * Query department by ID
     */
    @Operation(summary = "查询部门详情")
    @GetMapping("/{id}")
    public Result<Department> getDepartmentById(@PathVariable String id) {
        return departmentService.getDepartmentById(id);
    }

    /**
     * Create department
     */
    @Operation(summary = "创建部门")
    @PostMapping
    public Result<Department> createDepartment(@RequestBody Department department) {
        return departmentService.createDepartment(department);
    }

    /**
     * Update department
     */
    @Operation(summary = "更新部门")
    @PutMapping("/{id}")
    public Result<Department> updateDepartment(@PathVariable String id, @RequestBody Department department) {
        department.setDepartmentId(id);
        return departmentService.updateDepartment(department);
    }

    /**
     * Delete department
     */
    @Operation(summary = "删除部门")
    @DeleteMapping("/{id}")
    public Result<Boolean> deleteDepartment(@PathVariable String id) {
        return departmentService.deleteDepartment(id);
    }
}
