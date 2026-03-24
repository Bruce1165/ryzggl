package com.ryzggl;

import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

/**
 * RYZGGL - 人员资格管理系统
 * Main Application Class
 */
@SpringBootApplication
@MapperScan("com.ryzggl.repository")
public class RyzgglJavaApplication {

    public static void main(String[] args) {
        SpringApplication.run(RyzgglJavaApplication.class, args);
        System.out.println("========================================");
        System.out.println("RYZGGL Backend Started Successfully");
        System.out.println("API: http://localhost:8080/api");
        System.out.println("Swagger: http://localhost:8080/doc.html");
        System.out.println("========================================");
    }
}
