package com.ryzggl.service;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Worker;
import com.ryzggl.repository.WorkerRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

/**
 * Worker Service
 * Worker management
 */
@Service
public class WorkerService extends ServiceImpl<WorkerRepository, Worker> {

    private final WorkerRepository workerRepository;

    public WorkerService(WorkerRepository workerRepository) {
        this.workerRepository = workerRepository;
    }

    /**
     * Query worker list with pagination
     */
    public Result<IPage<Worker>> getWorkerList(Integer current, Integer size,
                                              String name, String idCard, String unitCode) {
        Page<Worker> page = new Page<>(current, size);
        IPage<Worker> result = workerRepository.selectWorkerPage(page, name, idCard, unitCode);
        return Result.success(result);
    }

    /**
     * Query worker by ID
     */
    public Result<Worker> getWorkerById(Long id) {
        Worker worker = workerRepository.selectById(id);
        if (worker == null) {
            return Result.error("人员不存在");
        }
        return Result.success(worker);
    }

    /**
     * Query worker by ID card
     */
    public Result<Worker> getWorkerByIdCard(String idCard) {
        Worker worker = workerRepository.selectByIdCard(idCard);
        if (worker == null) {
            return Result.error("人员不存在");
        }
        return Result.success(worker);
    }

    /**
     * Create worker
     */
    @Transactional
    public Result<Worker> createWorker(Worker worker) {
        // Generate worker code
        String workerCode = generateWorkerCode();
        worker.setWorkerCode(workerCode);

        worker.setCreateBy("system");
        worker.setUpdateBy("system");
        worker.setStatus("正常");

        save(worker);
        return Result.success("人员创建成功", worker);
    }

    /**
     * Update worker
     */
    @Transactional
    public Result<Void> updateWorker(Worker worker) {
        Worker existWorker = workerRepository.selectById(worker.getId());
        if (existWorker == null) {
            return Result.error("人员不存在");
        }

        updateById(worker);
        return Result.success("人员更新成功");
    }

    /**
     * Delete worker
     */
    @Transactional
    public Result<Void> deleteWorker(Long id) {
        Worker worker = workerRepository.selectById(id);
        if (worker == null) {
            return Result.error("人员不存在");
        }

        removeById(id);
        return Result.success("人员已删除");
    }

    /**
     * Generate worker code
     */
    private String generateWorkerCode() {
        return "WK" + System.currentTimeMillis();
    }
}
