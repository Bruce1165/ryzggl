package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Worker;
import com.ryzggl.service.WorkerService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.web.bind.annotation.*;

/**
 * Worker Controller
 * Worker management endpoints
 */
@Tag(name = "人员管理", description = "人员相关接口")
@RestController
@RequestMapping("/api/worker")
public class WorkerController {

    private final WorkerService workerService;

    public WorkerController(WorkerService workerService) {
        this.workerService = workerService;
    }

    public static class WorkerQuery {
        private Integer current = 1;
        private Integer size = 10;
        private String name;
        private String idCard;
        private String unitCode;

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

        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }

        public String getIdCard() {
            return idCard;
        }

        public void setIdCard(String idCard) {
            this.idCard = idCard;
        }

        public String getUnitCode() {
            return unitCode;
        }

        public void setUnitCode(String unitCode) {
            this.unitCode = unitCode;
        }
    }

    /**
     * Query worker list
     */
    @Operation(summary = "查询人员列表")
    @GetMapping("/list")
    public Result<IPage<Worker>> getWorkerList(WorkerQuery query) {
        return workerService.getWorkerList(query.getCurrent(), query.getSize(),
                query.getName(), query.getIdCard(), query.getUnitCode());
    }

    /**
     * Query worker by ID
     */
    @Operation(summary = "查询人员详情")
    @GetMapping("/{id}")
    public Result<Worker> getWorkerById(@PathVariable Long id) {
        return workerService.getWorkerById(id);
    }

    /**
     * Query worker by ID card
     */
    @Operation(summary = "根据身份证号查询人员")
    @GetMapping("/idcard/{idCard}")
    public Result<Worker> getWorkerByIdCard(@PathVariable String idCard) {
        return workerService.getWorkerByIdCard(idCard);
    }

    /**
     * Create worker
     */
    @Operation(summary = "创建人员")
    @PostMapping
    public Result<Worker> createWorker(@RequestBody Worker worker) {
        return workerService.createWorker(worker);
    }

    /**
     * Update worker
     */
    @Operation(summary = "更新人员")
    @PutMapping
    public Result<Void> updateWorker(@RequestBody Worker worker) {
        return workerService.updateWorker(worker);
    }

    /**
     * Delete worker
     */
    @Operation(summary = "删除人员")
    @DeleteMapping("/{id}")
    public Result<Void> deleteWorker(@PathVariable Long id) {
        return workerService.deleteWorker(id);
    }
}
