import { createAsyncThunk } from "@reduxjs/toolkit";
import axiosInstance from "../../api/axiosInstance";
import { AUTH_API } from "../../api/auth/authAPI";

export const authActions = {
    login: createAsyncThunk("login", async (request) => {
        const response = await axiosInstance.post(AUTH_API.LOGIN, request);
        return response.data;
    }),
    register: createAsyncThunk("register", async (request) => {
        const response = await axiosInstance.post(AUTH_API.REGISTER, request);
        return response.data;
    }),
    getCurrentUser: createAsyncThunk("me", async () => {
        const response = await axiosInstance.get(AUTH_API.ME,{passError : true});
        return response.data;
    }),
};
