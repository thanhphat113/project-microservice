package com.demo.TaskService.Repositories;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

import com.demo.TaskService.Models.Task;

public interface TaskRepository extends JpaRepository<Task, Integer> {
	List<Task> findByUserId(Integer userId);
}
