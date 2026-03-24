package com.ryzggl.common;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.fasterxml.jackson.annotation.JsonFormat;
import java.time.LocalDateTime;

/**
 * Base entity class for all database tables
 * Maps common fields: create/modify person and timestamps
 */
public class BaseEntity {

    /**
     * Primary key - auto-incrementing bigint ID
     */
    @TableId(type = IdType.AUTO)
    private Long id;

    /**
     * Creator person ID
     * Maps to: CREATEPERSONID, CREATERECORDID
     */
    @TableField("createpersonid")
    private Long createPersonId;

    /**
     * Creation time
     * Maps to: CREATETIME, CREATEDATE
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss", timezone = "GMT+8")
    @TableField("createtime")
    private LocalDateTime createTime;

    /**
     * Last modify person ID
     * Maps to: MODIFYPERSONID, MODIFYRECORDID
     */
    @TableField("modifypersonid")
    private Long modifyPersonId;

    /**
     * Last modify time
     * Maps to: MODIFYTIME
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss", timezone = "GMT+8")
    @TableField("modifytime")
    private LocalDateTime modifyTime;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getCreatePersonId() {
        return createPersonId;
    }

    public void setCreatePersonId(Long createPersonId) {
        this.createPersonId = createPersonId;
    }

    public LocalDateTime getCreateTime() {
        return createTime;
    }

    public void setCreateTime(LocalDateTime createTime) {
        this.createTime = createTime;
    }

    public Long getModifyPersonId() {
        return modifyPersonId;
    }

    public void setModifyPersonId(Long modifyPersonId) {
        this.modifyPersonId = modifyPersonId;
    }

    public LocalDateTime getModifyTime() {
        return modifyTime;
    }

    public void setModifyTime(LocalDateTime modifyTime) {
        this.modifyTime = modifyTime;
    }
}
