package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import lombok.EqualsAndHashCode;

import java.util.Date;
import java.util.List;

/**
 * ApplyCheckTask Entity - Application Check Task (抽查任务)
 * Represents a quality check/audit task for applications
 */
@Data
@EqualsAndHashCode(callSuper = false)
@TableName("ApplyCheckTask")
public class ApplyCheckTask {

    /**
     * Task ID - Primary Key
     */
    @TableId(type = IdType.AUTO)
    private Long TaskID;

    /**
     * Creator - Created by user
     */
    private String cjr;

    /**
     * Create Time - Task creation timestamp
     */
    private Date cjsj;

    /**
     * Business Range - Business scope/range description
     * e.g., "安管人员,特种作业"
     */
    private String BusRange;

    /**
     * Business Range Code - Business type codes (comma-separated)
     * e.g., "1,2,3,4" (1=二建, 2=二造, 3=安管人员, 4=特种作业)
     */
    private String BusRangeCode;

    /**
     * Business Start Date - Start date of business period to check
     */
    private Date BusStartDate;

    /**
     * Business End Date - End date of business period to check
     */
    private Date BusEndDate;

    /**
     * Check Percentage - Percentage of items to sample (x/1000)
     * e.g., 10 = 1% sampling, 100 = 10% sampling
     */
    private Integer CheckPer;

    /**
     * Item Count - Number of items in this check task (calculated)
     */
    private Integer ItemCount;

    /**
     * Check items in this task (lazy loaded)
     */
    private List<ApplyCheckTaskItem> items;
}
