package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.FileInfo;
import com.ryzggl.service.FileService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.FileSystemResource;
import org.springframework.core.io.Resource;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.nio.file.Path;
import java.util.List;

/**
 * File Controller - REST API for file upload/download
 * Maps to: FileInfo module in .NET system
 */
@RestController
@RequestMapping("/api/file")
@Tag(name = "File Management", description = "File upload, download, and management")
public class FileController {

    private static final Logger log = LoggerFactory.getLogger(FileController.class);

    @Autowired
    private FileService fileService;

    /**
     * Upload file
     * Maps to: uploader/Upload.aspx in .NET system
     */
    @Operation(summary = "Upload file", description = "Upload a file with business association")
    @PostMapping("/upload")
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    public Result<FileInfo> uploadFile(
            @RequestParam("file") MultipartFile file,
            @RequestParam(value = "businessId", required = false) Long businessId,
            @RequestParam(value = "recordId", required = false) Long recordId,
            @RequestParam(value = "uploadPersonId", required = false) Long uploadPersonId) {

        try {
            // Validate file size
            if (file.isEmpty()) {
                return Result.error("File is empty");
            }

            if (file.getSize() > 100 * 1024 * 1024) {
                return Result.error("File size exceeds 100MB limit");
            }

            FileInfo fileInfo = fileService.uploadFile(
                    file.getInputStream(),
                    file.getOriginalFilename(),
                    businessId,
                    recordId,
                    uploadPersonId
            );

            if (fileInfo == null) {
                return Result.error("File upload failed - validation error");
            }

            return Result.success(fileInfo);

        } catch (IOException e) {
            log.error("File upload error: {}", e.getMessage());
            return Result.error("File upload error: " + e.getMessage());
        }
    }

    /**
     * Download file
     * Maps to: File download in .NET system
     */
    @Operation(summary = "Download file", description = "Download a file by ID")
    @GetMapping("/download/{fileId}")
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    public ResponseEntity<Resource> downloadFile(@PathVariable Long fileId) {
        try {
            Path filePath = fileService.downloadFile(fileId);
            FileInfo fileInfo = fileService.getById(fileId);

            Resource resource = new FileSystemResource(filePath);

            String contentType = fileInfo != null ? fileInfo.getContentType() : "application/octet-stream";
            String originalFilename = fileInfo != null ? fileInfo.getFileName() : "download";

            return ResponseEntity.ok()
                    .contentType(MediaType.parseMediaType(contentType))
                    .header(HttpHeaders.CONTENT_DISPOSITION,
                            "attachment; filename=\"" + originalFilename + "\"")
                    .body(resource);

        } catch (RuntimeException e) {
            log.error("File download error: {}", e.getMessage());
            return ResponseEntity.notFound().build();
        }
    }

    /**
     * Get files by record ID
     * Maps to: FileInfoDAL.GetFilesByRecordID
     */
    @Operation(summary = "Get files by record", description = "Get all files associated with a business record")
    @GetMapping("/record/{recordId}")
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    public Result<List<FileInfo>> getFilesByRecordId(@PathVariable Long recordId) {
        return fileService.getFilesByRecordId(recordId);
    }

    /**
     * Get files by business ID
     * Maps to: FileInfoDAL.GetFilesByBusinessID
     */
    @Operation(summary = "Get files by business", description = "Get all files associated with a business entity")
    @GetMapping("/business/{businessId}")
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    public Result<List<FileInfo>> getFilesByBusinessId(@PathVariable Long businessId) {
        return fileService.getFilesByBusinessId(businessId);
    }

    /**
     * Delete file
     * Maps to: FileInfoDAL.Delete
     */
    @Operation(summary = "Delete file", description = "Delete a file by ID")
    @DeleteMapping("/{fileId}")
    @PreAuthorize("hasRole('ADMIN')")
    public Result<Void> deleteFile(@PathVariable Long fileId) {
        return fileService.deleteFile(fileId);
    }

    /**
     * Validate file before upload
     */
    @Operation(summary = "Validate file", description = "Check if file is valid for upload")
    @PostMapping("/validate")
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    public Result<Void> validateFile(@RequestParam("file") MultipartFile file) {
        if (file.isEmpty()) {
            return Result.error("File is empty");
        }

        String filename = file.getOriginalFilename();
        if (filename == null) {
            return Result.error("Invalid filename");
        }

        // Check file extension
        int lastDotIndex = filename.lastIndexOf('.');
        if (lastDotIndex == -1) {
            return Result.error("File has no extension");
        }

        String extension = filename.substring(lastDotIndex).toLowerCase();
        List<String> allowedExtensions = List.of(
                ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt", ".jpg", ".jpeg", ".png", ".gif", ".bmp"
        );

        if (!allowedExtensions.contains(extension)) {
            return Result.error("File type not allowed: " + extension);
        }

        // Check file size
        if (file.getSize() > 100 * 1024 * 1024) {
            return Result.error("File size exceeds 100MB limit");
        }

        return Result.success();
    }
}
