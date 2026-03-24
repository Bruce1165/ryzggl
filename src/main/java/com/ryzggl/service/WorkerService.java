package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.Worker;
import com.ryzggl.repository.WorkerRepository;
import com.ryzggl.common.Result;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * Worker Service - Business logic for worker/personnel management
 */
@Service
@Transactional
public class WorkerService extends ServiceImpl<WorkerRepository, Worker> {

    private static final Logger log = LoggerFactory.getLogger(WorkerService.class);

    /**
     * Create new worker
     */
    public Result<Void> createWorker(Worker worker) {
        log.info("Creating worker: {}", worker.getWorkerName());

        // Set initial status if not provided
        if (worker.getWorkerStatus() == null) {
            worker.setWorkerStatus("在职");
        }

        boolean success = save(worker);
        if (success) {
            log.info("Worker created successfully: {}", worker.getId());
            return Result.success();
        } else {
            log.error("Failed to create worker: {}", worker.getId());
            return Result.error("Failed to create worker");
        }
    }

    /**
     * Update worker information
     */
    public Result<Void> updateWorker(Worker worker) {
        log.info("Updating worker: {}", worker.getId());

        boolean success = updateById(worker);
        if (success) {
            log.info("Worker updated successfully: {}", worker.getId());
            return Result.success();
        } else {
            log.error("Failed to update worker: {}", worker.getId());
            return Result.error("Failed to update worker");
        }
    }

    /**
     * Get workers by department
     */
    public Result<List<Worker>> getWorkersByUnitCode(String unitCode) {
        log.debug("Getting workers by unit: {}", unitCode);
        List<Worker> workers = lambdaQuery()
                .eq(Worker::getUnitCode, unitCode)
                .orderByDesc(Worker::getId)
                .list();

        return Result.success(workers);
    }

    /**
     * Get workers by status
     */
    public Result<List<Worker>> getWorkersByStatus(String status) {
        log.debug("Getting workers by status: {}", status);
        List<Worker> workers = lambdaQuery()
                .eq(Worker::getWorkerStatus, status)
                .orderByDesc(Worker::getId)
                .list();

        return Result.success(workers);
    }

    /**
     * Get worker by ID
     */
    public Result<Worker> getWorker(Long id) {
        log.debug("Getting worker: {}", id);
        Worker worker = getById(id);
        return worker != null ? Result.success(worker) : Result.error("Worker not found");
    }

    /**
     * Delete worker (soft delete by status)
     */
    public Result<Void> deleteWorker(Long id) {
        log.info("Deleting worker: {}", id);

        Worker worker = getById(id);
        if (worker == null) {
            return Result.error("Worker not found");
        }

        // Soft delete by updating status
        worker.setWorkerStatus("离职");
        boolean success = updateById(worker);

        if (success) {
            log.info("Worker deleted successfully: {}", id);
            return Result.success();
        } else {
            log.error("Failed to delete worker: {}", id);
            return Result.error("Failed to delete worker");
        }
    }

    /**
     * Get worker count by department
     */
    public Result<Long> getWorkerCount(String unitCode) {
        Long count = lambdaQuery()
                .eq(Worker::getUnitCode, unitCode)
                .count();

        return Result.success(count);
    }
}
