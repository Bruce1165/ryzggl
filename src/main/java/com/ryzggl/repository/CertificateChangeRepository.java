package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificateChange;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

/**
 * CertificateChange Repository
 * Data access for certificate change history
 */
@Mapper
public interface CertificateChangeRepository extends BaseMapper<CertificateChange> {
}
