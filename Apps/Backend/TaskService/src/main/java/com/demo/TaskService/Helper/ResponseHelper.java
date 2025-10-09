package com.demo.TaskService.Helper;

import org.springframework.http.ResponseEntity;
import com.demo.TaskService.DTOs.Responses.ControllerResponse;

public class ResponseHelper {

	public static <T> ResponseEntity<ControllerResponse<T>> ok(T data, String message) {
		return ResponseEntity.ok(ControllerResponse.success(data, message));
	}

	public static <T> ResponseEntity<ControllerResponse<T>> badRequest(String message) {
		return ResponseEntity.badRequest().body(ControllerResponse.failure(message));
	}

	public static <T> ResponseEntity<ControllerResponse<T>> notFound(String message) {
		return ResponseEntity.notFound().build();
	}

	public static <T> ResponseEntity<ControllerResponse<T>> internalError(String message) {
		return ResponseEntity.internalServerError()
				.body(ControllerResponse.failure(message));
	}
}
