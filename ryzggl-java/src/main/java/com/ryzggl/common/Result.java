package com.ryzggl.common;

import java.io.Serializable;

/**
 * Unified API Response Wrapper
 * @param <T> Data type
 */
public class Result<T> implements Serializable {

    private static final long serialVersionUID = 1L;

    /**
     * Response code
     * 0 - success
     * 1 - business error
     * 2 - unauthorized
     * 3 - forbidden
     */
    private Integer code;

    /**
     * Response message
     */
    private String message;

    /**
     * Response data
     */
    private T data;

    /**
     * Timestamp
     */
    private Long timestamp;

    public Result() {
        this.timestamp = System.currentTimeMillis();
    }

    public Result(Integer code, String message, T data) {
        this.code = code;
        this.message = message;
        this.data = data;
        this.timestamp = System.currentTimeMillis();
    }

    // Getters and Setters
    public Integer getCode() {
        return code;
    }

    public void setCode(Integer code) {
        this.code = code;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public T getData() {
        return data;
    }

    public void setData(T data) {
        this.data = data;
    }

    public Long getTimestamp() {
        return timestamp;
    }

    public void setTimestamp(Long timestamp) {
        this.timestamp = timestamp;
    }

    /**
     * Success response
     */
    public static <T> Result<T> success() {
        return new Result<>(0, "success", null);
    }

    public static <T> Result<T> success(T data) {
        return new Result<>(0, "success", data);
    }

    public static <T> Result<T> success(String message, T data) {
        return new Result<>(0, message, data);
    }

    /**
     * Success response with message only (returns Result<Void>)
     */
    public static Result<Void> success(String message) {
        return new Result<>(0, message, null);
    }

    /**
     * Error response
     */
    public static <T> Result<T> error(String message) {
        return new Result<>(1, message, null);
    }

    public static <T> Result<T> error(Integer code, String message) {
        return new Result<>(code, message, null);
    }

    /**
     * Unauthorized response
     */
    public static <T> Result<T> unauthorized(String message) {
        return new Result<>(2, message, null);
    }

    /**
     * Forbidden response
     */
    public static <T> Result<T> forbidden(String message) {
        return new Result<>(3, message, null);
    }
}
