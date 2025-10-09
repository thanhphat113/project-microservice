package com.demo.TaskService.Controllers;

import java.util.List;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.demo.TaskService.DTOs.Requests.TaskCreateRequest;
import com.demo.TaskService.DTOs.Requests.TaskUpdateRequest;
import com.demo.TaskService.DTOs.Responses.ControllerResponse;
import com.demo.TaskService.DTOs.Responses.TaskResponse;

import com.demo.TaskService.Services.ITaskService;

import static com.demo.TaskService.Helper.ResponseHelper.*;

@RestController
@RequestMapping("/api/tasks")
public class TaskController {
	private final ITaskService taskService;

	public TaskController(ITaskService taskService) {
		this.taskService = taskService;
	}

	@GetMapping("/tasks")
	public ResponseEntity<ControllerResponse<List<TaskResponse>>> getTasksByUserId(
			@RequestHeader("X-User-Id") int userId) {
		var getTasks = taskService.findByUserId(userId);
		if (!getTasks.isSuccess()) {
			return badRequest(getTasks.getMessage());
		}

		return ok(getTasks.getData(), getTasks.getMessage());
	}

	@PostMapping
	public ResponseEntity<ControllerResponse<TaskResponse>> createTask(
			@RequestHeader("X-User-Id") String userId,
			@RequestBody TaskCreateRequest request) {

		request.setUserId(Integer.parseInt(userId));
		var createTask = taskService.createTask(request);
		if (!createTask.isSuccess())
			return badRequest(createTask.getMessage());

		return ok(createTask.getData(), createTask.getMessage());

	}

	@PutMapping("/update")
	public ResponseEntity<ControllerResponse<TaskResponse>> updateTask(@RequestBody TaskUpdateRequest request) {
		var updateTask = taskService.updateTask(request);
		if (!updateTask.isSuccess())
			return badRequest(updateTask.getMessage());

		return ok(updateTask.getData(), updateTask.getMessage());

	}

	@DeleteMapping("/{id}")
	public ResponseEntity<ControllerResponse<Void>> deleteTask(@PathVariable Integer id) {
		var deleteTask = taskService.deleteTask(id);
		if (!deleteTask.isSuccess())
			return badRequest(deleteTask.getMessage());

		return ok(deleteTask.getData(), deleteTask.getMessage());
	}
}
