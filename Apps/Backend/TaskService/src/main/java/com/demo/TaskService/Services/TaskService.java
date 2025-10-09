package com.demo.TaskService.Services;

import java.util.List;
import java.util.Optional;

import org.springframework.stereotype.Service;

import com.demo.TaskService.DTOs.Requests.TaskCreateRequest;
import com.demo.TaskService.DTOs.Requests.TaskUpdateRequest;
import com.demo.TaskService.DTOs.Responses.ServiceResult;
import com.demo.TaskService.DTOs.Responses.TaskResponse;
import com.demo.TaskService.Mapper.TaskMapper;
import com.demo.TaskService.Models.Task;
import com.demo.TaskService.Repositories.TaskRepository;

@Service
public class TaskService implements ITaskService {
	private final TaskRepository taskRepo;
	private final TaskMapper taskMapper;

	public TaskService(TaskRepository taskRepo, TaskMapper taskMapper) {
		this.taskRepo = taskRepo;
		this.taskMapper = taskMapper;
	}

	@Override
	public ServiceResult<List<Task>> getAllTasks() {
		// TODO Auto-generated method stub
		throw new UnsupportedOperationException("Unimplemented method 'getAllTasks'");
	}

	@Override
	public ServiceResult<Task> getTaskById(Integer id) {
		try {
			Optional<Task> targetTask = taskRepo.findById(id);
			if (targetTask.isPresent()) {
				return ServiceResult.success(targetTask.get(), "Task retrieved successfully");
			}

			return ServiceResult.failure("Task not found");
		} catch (Exception e) {
			return ServiceResult.failure("Error while finding task: " + e.getMessage());
		}

	}

	@Override
	public ServiceResult<TaskResponse> createTask(TaskCreateRequest request) {
		try {
			if (request.getName() == null || request.getName().isBlank()) {
				return ServiceResult.failure("Task name cannot be empty");
			}

			Task task = taskMapper.toModel(request);
			Task saved = taskRepo.save(task);

			var response = taskMapper.toResponse(saved);
			return ServiceResult.success(response, "Task created successfully");
		} catch (Exception e) {
			return ServiceResult.failure("Error while creating task: " + e.getMessage());
		}

	}

	@Override
	public ServiceResult<TaskResponse> updateTask(TaskUpdateRequest taskUpdateRequest) {
		var getTask = getTaskById(taskUpdateRequest.getTaskId());
		if (!getTask.isSuccess())
			return ServiceResult.failure("Task not found");
		try {
			var targetTask = getTask.getData();

			taskMapper.updateTask(targetTask, taskUpdateRequest);
			taskRepo.save(targetTask);
			var response = taskMapper.toResponse(targetTask);
			return ServiceResult.success(response, "Task updated successfully");
		} catch (Exception e) {
			return ServiceResult.failure("Error while updating task: " + e.getMessage());

		}

	}

	@Override
	public ServiceResult<Void> deleteTask(Integer id) {
		var targetTask = getTaskById(id);
		if (!targetTask.isSuccess())
			return ServiceResult.failure("Task not found");

		try {
			taskRepo.delete(targetTask.getData());
			return ServiceResult.success(null, "Task deleted successfully");
		} catch (Exception e) {
			return ServiceResult.failure("Error while deleting task: " + e.getMessage());
		}
	}

	@Override
	public ServiceResult<List<TaskResponse>> findByUserId(Integer id) {
		if (id == null)
			return ServiceResult.failure("User ID can't be null");

		try {

			List<Task> targetList = taskRepo.findByUserId(id);

			var response = targetList.isEmpty() ? null : taskMapper.toResponse(targetList);

			return ServiceResult.success(response, "Tasks retrieved successfully");
		} catch (Exception e) {
			return ServiceResult.failure("Error while finding tasks: " + e.getMessage());

		}
	}

}
