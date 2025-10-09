package com.demo.TaskService.Mapper;

import java.util.List;

import org.mapstruct.Mapper;
import org.mapstruct.MappingTarget;

import com.demo.TaskService.DTOs.Requests.TaskCreateRequest;
import com.demo.TaskService.DTOs.Requests.TaskUpdateRequest;
import com.demo.TaskService.DTOs.Responses.TaskResponse;
import com.demo.TaskService.Models.Task;

@Mapper(componentModel = "spring")
public interface TaskMapper {
	Task toModel(TaskCreateRequest task);

	TaskResponse toResponse(Task task);

	List<TaskResponse> toResponse(List<Task> list);

	void updateTask(@MappingTarget Task task, TaskUpdateRequest request);
}
