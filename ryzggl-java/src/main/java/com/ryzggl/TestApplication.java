package com.ryzggl;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class TestApplication {
    public static void main(String[] args) {
        System.out.println("========================================");
        System.out.println("RYZGGL Backend - Simple Test");
        System.out.println("========================================");
        SpringApplication.run(TestApplication.class, args);
        System.out.println("Application started!");
        System.out.println("========================================");
    }
}
