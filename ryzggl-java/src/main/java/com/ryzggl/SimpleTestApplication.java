package com.ryzggl;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@SpringBootApplication
@RestController
public class SimpleTestApplication {

    public static void main(String[] args) {
        SpringApplication.run(SimpleTestApplication.class, args);
    }

    @GetMapping("/")
    public String home() {
        return "RYZGGL Backend is Running! Java " + System.getProperty("java.version");
    }

    @GetMapping("/api/health")
    public String health() {
        return "{\"status\":\"ok\"}";
    }

    @GetMapping("/api/test")
    public String test() {
        return "{\"message\":\"test endpoint working\",\"data\":{\"name\":\"SimpleTestApplication\"}";
    }
}
