package com.ryzggl;

import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

/**
 * RYZGGL Application Main Class
 * Entry point for the Spring Boot application
 */
@SpringBootApplication
@MapperScan("com.ryzggl.repository")
public class RyzgglApplication {

    public static void main(String[] args) {
        SpringApplication.run(RyzgglApplication.class, args);
    }
}
