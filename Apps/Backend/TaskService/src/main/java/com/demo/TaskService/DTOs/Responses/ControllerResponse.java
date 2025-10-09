package com.demo.TaskService.DTOs.Responses;

import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.experimental.FieldDefaults;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
@FieldDefaults(level = AccessLevel.PRIVATE)
public class ControllerResponse<T> {
	String message;
	T data;

	public static <T> ControllerResponse<T> success(T data, String message) {
		return new ControllerResponse<>(message, data);
	}

	public static <T> ControllerResponse<T> failure(String message) {
		return new ControllerResponse<>(message, null);
	}
}
