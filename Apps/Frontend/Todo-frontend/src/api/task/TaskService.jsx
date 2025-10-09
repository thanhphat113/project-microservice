import axiosInstance from "../axiosInstance";
import { TASK_API } from "./taskAPI";

export const taskService = {
    addTask: async (request) => {
        const response = await axiosInstance.post(TASK_API.CREATE, request, {showSuccess: true});
        return response.data;
    },
    getTasksByUserId: async () => {
        const response = await axiosInstance.get(TASK_API.GET_TASKS_BY_USER_ID);
        return response.data;
    },
    deleteTask: async (id) => {
        const response = await axiosInstance.delete(`${TASK_API.DELETE}/${id}`, {showSuccess: true});
        return response.data;
    },
    updateTask: async (request) => {
        const response = await axiosInstance.put(TASK_API.UPDATE, request, {showSuccess: true});
        return response.data;
    },
};
