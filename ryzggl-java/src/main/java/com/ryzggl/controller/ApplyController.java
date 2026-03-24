package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Apply;
import com.ryzggl.service.ApplyService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.web.bind.annotation.*;

/**
 * Apply Controller
 * Application management endpoints
 */
@Tag(name = "申请管理", description = "申请相关接口")
@RestController
@RequestMapping("/api/apply")
public class ApplyController {

    private final ApplyService applyService;

    public ApplyController(ApplyService applyService) {
        this.applyService = applyService;
    }

    public static class ApplyQuery {
        private Integer current = 1;
        private Integer size = 10;
        private String applyName;
        private String applyType;
        private String applyStatus;

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

        public String getApplyName() {
            return applyName;
        }

        public void setApplyName(String applyName) {
            this.applyName = applyName;
        }

        public String getApplyType() {
            return applyType;
        }

        public void setApplyType(String applyType) {
            this.applyType = applyType;
        }

        public String getApplyStatus() {
            return applyStatus;
        }

        public void setApplyStatus(String applyStatus) {
            this.applyStatus = applyStatus;
        }
    }

    public static class ApprovalRequest {
        private Long id;
        private String approvalOpinion;
        private String approver;

        public Long getId() {
            return id;
        }

        public void setId(Long id) {
            this.id = id;
        }

        public String getApprovalOpinion() {
            return approvalOpinion;
        }

        public void setApprovalOpinion(String approvalOpinion) {
            this.approvalOpinion = approvalOpinion;
        }

        public String getApprover() {
            return approver;
        }

        public void setApprover(String approver) {
            this.approver = approver;
        }
    }

    /**
     * Query apply list
     */
    @Operation(summary = "查询申请列表")
    @GetMapping("/list")
    public Result<IPage<Apply>> getApplyList(ApplyQuery query) {
        return applyService.getApplyList(query.getCurrent(), query.getSize(),
                query.getApplyName(), query.getApplyType(), query.getApplyStatus());
    }

    /**
     * Query apply by ID
     */
    @Operation(summary = "查询申请详情")
    @GetMapping("/{id}")
    public Result<Apply> getApplyById(@PathVariable Long id) {
        return applyService.getApplyById(id);
    }

    /**
     * Create apply
     */
    @Operation(summary = "创建申请")
    @PostMapping
    public Result<Apply> createApply(@RequestBody Apply apply) {
        return applyService.createApply(apply);
    }

    /**
     * Update apply
     */
    @Operation(summary = "更新申请")
    @PutMapping
    public Result<Void> updateApply(@RequestBody Apply apply) {
        return applyService.updateApply(apply);
    }

    /**
     * Submit apply
     */
    @Operation(summary = "提交申请")
    @PutMapping("/{id}/submit")
    public Result<Void> submitApply(@PathVariable Long id) {
        return applyService.submitApply(id);
    }

    /**
     * Approve apply
     */
    @Operation(summary = "审批通过申请")
    @PutMapping("/approve")
    public Result<Void> approveApply(@RequestBody ApprovalRequest request) {
        return applyService.approveApply(request.getId(), request.getApprovalOpinion(), request.getApprover());
    }

    /**
     * Reject apply
     */
    @Operation(summary = "驳回申请")
    @PutMapping("/reject")
    public Result<Void> rejectApply(@RequestBody ApprovalRequest request) {
        return applyService.rejectApply(request.getId(), request.getApprovalOpinion(), request.getApprover());
    }

    /**
     * Delete apply
     */
    @Operation(summary = "删除申请")
    @DeleteMapping("/{id}")
    public Result<Void> deleteApply(@PathVariable Long id) {
        return applyService.deleteApply(id);
    }
}
