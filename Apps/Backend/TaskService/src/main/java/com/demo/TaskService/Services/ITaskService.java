package com.demo.TaskService.Services;

import java.util.List;

import com.demo.TaskService.DTOs.Requests.TaskCreateRequest;
import com.demo.TaskService.DTOs.Requests.TaskUpdateRequest;
import com.demo.TaskService.DTOs.Responses.ServiceResult;
import com.demo.TaskService.DTOs.Responses.TaskResponse;
import com.demo.TaskService.Models.Task;

public interface ITaskService {
	ServiceResult<List<Task>> getAllTasks();

	ServiceResult<Task> getTaskById(Integer id);

	ServiceResult<List<TaskResponse>> findByUserId(Integer id);

	ServiceResult<TaskResponse> createTask(TaskCreateRequest task);

	ServiceResult<TaskResponse> updateTask(TaskUpdateRequest task);

	ServiceResult<Void> deleteTask(Integer id);
}
