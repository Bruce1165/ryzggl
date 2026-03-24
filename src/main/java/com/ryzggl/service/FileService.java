package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.FileInfo;
import com.ryzggl.repository.FileInfoRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.io.IOException;
import java.io.InputStream;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;
import java.time.LocalDateTime;
import java.util.Arrays;
import java.util.List;
import java.util.UUID;

/**
 * File Service - Business logic for file upload/download
 */
@Service
@Transactional
public class FileService extends ServiceImpl<FileInfoRepository, FileInfo> {

    private static final Logger log = LoggerFactory.getLogger(FileService.class);

    // Allowed file extensions for upload
    private static final List<String> ALLOWED_EXTENSIONS = Arrays.asList(
            ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt", ".jpg", ".jpeg", ".png", ".gif", ".bmp"
    );

    // Maximum file size (100MB)
    private static final long MAX_FILE_SIZE = 100 * 1024 * 1024;

    // File upload directory (configurable via application.yml)
    @Value("${file.upload.path:./uploads/}")
    private String uploadPath;

    /**
     * Upload file with validation
     * @return FileInfo if successful, null if validation fails
     */
    @Transactional
    public FileInfo uploadFile(InputStream inputStream, String originalFilename,
                              Long businessId, Long recordId, Long uploadPersonId) throws IOException {
        // Validate file extension
        String fileExtension = getFileExtension(originalFilename);
        if (fileExtension == null) {
            log.error("Cannot determine file extension: {}", originalFilename);
            return null;
        }

        if (!ALLOWED_EXTENSIONS.contains(fileExtension)) {
            log.error("File extension not allowed: {}", fileExtension);
            return null;
        }

        // Generate unique filename
        String uniqueFileName = UUID.randomUUID().toString();
        String storedFileName = uniqueFileName + fileExtension;

        // Create upload directory if not exists
        Path uploadDirectory = Paths.get(uploadPath);
        if (!Files.exists(uploadDirectory)) {
            Files.createDirectories(uploadDirectory);
        }

        Path destination = uploadDirectory.resolve(storedFileName);

        try {
            // Save file to disk
            long fileSize = Files.copy(inputStream, destination, StandardCopyOption.REPLACE_EXISTING);

            // Create file info record
            FileInfo fileInfo = new FileInfo();
            fileInfo.setFileName(originalFilename);
            fileInfo.setFilePath(storedFileName);
            fileInfo.setFileSize(fileSize);
            fileInfo.setFileType(fileExtension.replace(".", ""));
            fileInfo.setContentType(getContentType(fileExtension));
            fileInfo.setBusinessId(businessId);
            fileInfo.setRecordId(recordId);
            fileInfo.setUploadPersonId(uploadPersonId);
            fileInfo.setUploadTime(LocalDateTime.now().toString());

            boolean saveResult = save(fileInfo);

            if (saveResult) {
                log.info("File uploaded successfully: {}", storedFileName);
                return fileInfo;
            } else {
                log.error("Failed to save file info record: {}", originalFilename);
                // Clean up uploaded file if DB save failed
                Files.deleteIfExists(destination);
                return null;
            }
        } catch (IOException e) {
            log.error("File upload failed: {}", e.getMessage());
            throw new IOException("File upload failed: " + e.getMessage(), e);
        }
    }

    /**
     * Download file by ID
     * @return Path to the file if successful, null if not found
     */
    public Path downloadFile(Long fileId) {
        log.info("Downloading file: {}", fileId);

        FileInfo fileInfo = baseMapper.getById(fileId);
        if (fileInfo == null) {
            log.error("File not found: {}", fileId);
            throw new RuntimeException("File not found");
        }

        String filePath = fileInfo.getFilePath();
        if (filePath == null) {
            log.error("File path is null for file: {}", fileId);
            throw new RuntimeException("File path is null for file: " + fileId);
        }

        try {
            Path uploadDirectory = Paths.get(uploadPath).toAbsolutePath().normalize();
            Path path = uploadDirectory.resolve(filePath).normalize();

            // Validate file is within project directory
            if (!path.startsWith(uploadDirectory)) {
                log.error("Security violation: File outside project directory");
                throw new RuntimeException("Security violation: File outside project directory");
            }

            if (!Files.exists(path)) {
                log.error("File does not exist on disk: {}", path);
                throw new RuntimeException("File does not exist on disk");
            }

            return path;

        } catch (java.nio.file.InvalidPathException e) {
            log.error("Failed to download file: {}", e.getMessage());
            throw new RuntimeException("Failed to download file: " + e.getMessage());
        }
    }

    /**
     * Delete file (soft delete)
     * @return success/error result
     */
    @Transactional
    public Result<Void> deleteFile(Long fileId) {
        log.info("Deleting file: {}", fileId);

        FileInfo fileInfo = baseMapper.getById(fileId);
        if (fileInfo == null) {
            return Result.error("File not found");
        }

        try {
            Path uploadDirectory = Paths.get(uploadPath).toAbsolutePath().normalize();
            Path filePath = uploadDirectory.resolve(fileInfo.getFilePath()).normalize();

            if (Files.exists(filePath)) {
                Files.delete(filePath);
                log.info("File deleted from disk: {}", filePath);
            }

            // Soft delete from database
            removeById(fileId);

            log.info("File record deleted: {}", fileId);
            return Result.success();
        } catch (Exception e) {
            log.error("Failed to delete file: {}", e.getMessage());
            return Result.error("Failed to delete file: " + e.getMessage());
        }
    }

    /**
     * Query files by business record
     */
    public Result<List<FileInfo>> getFilesByRecordId(Long recordId) {
        log.debug("Getting files for record: {}", recordId);
        List<FileInfo> files = baseMapper.findByRecordId(recordId);
        return Result.success(files);
    }

    /**
     * Query files by business entity
     */
    public Result<List<FileInfo>> getFilesByBusinessId(Long businessId) {
        log.debug("Getting files for business: {}", businessId);
        List<FileInfo> files = baseMapper.findByBusinessId(businessId);
        return Result.success(files);
    }

    /**
     * Validate file extension
     * Returns the extension with the dot
     */
    private String getFileExtension(String filename) {
        if (filename == null) {
            return null;
        }

        int lastDotIndex = filename.lastIndexOf('.');
        if (lastDotIndex == -1 || lastDotIndex == filename.length() - 1) {
            return null;
        }
        return filename.substring(lastDotIndex).toLowerCase();
    }

    /**
     * Get content type based on file extension
     */
    private String getContentType(String fileExtension) {
        if (fileExtension == null) {
            return "application/octet-stream";
        }

        switch (fileExtension.toLowerCase()) {
            case ".pdf":
                return "application/pdf";
            case ".doc":
                return "application/msword";
            case ".docx":
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            case ".xls":
                return "application/vnd.ms-excel";
            case ".xlsx":
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            case ".jpg":
            case ".jpeg":
                return "image/jpeg";
            case ".png":
                return "image/png";
            case ".gif":
                return "image/gif";
            case ".bmp":
                return "image/bmp";
            case ".txt":
                return "text/plain";
            default:
                return "application/octet-stream";
        }
    }

    /**
     * Validate file size
     * Returns false if file exceeds MAX_FILE_SIZE
     */
    private boolean validateFileSize(long fileSize) {
        return fileSize <= MAX_FILE_SIZE;
    }
}
