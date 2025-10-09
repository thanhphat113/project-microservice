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
public class ServiceResult<T> {
	boolean isSuccess;
	String message;
	T data;

	public static <T> ServiceResult<T> success(T data, String message) {
		return new ServiceResult<>(true, message, data);
	}

	public static <T> ServiceResult<T> failure(String message) {
		return new ServiceResult<>(false, message, null);
	}
}
