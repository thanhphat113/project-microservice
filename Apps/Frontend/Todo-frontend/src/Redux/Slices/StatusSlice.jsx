import { createSlice } from "@reduxjs/toolkit";
import { authActions } from "../Actions/AuthActions";

const StatusSlice = createSlice({
    name: "status",
    initialState: {
        isSuccess: false,
        isError: false,
        isLoading: true,
        message: "",
    },
    reducers: {
        handleErrorRequest: (state, action) => {
            state.isSuccess = false
            state.isLoading = false
            state.isError = true
			state.message = action.payload
        },
		handleResetStatus: (state) => {
			state.isSuccess = false
			state.isError = false
			state.isLoading = false
            state.message = "";
		},
        handleSuccessStatus: (state, action) => {
			state.isSuccess = true
			state.isError = false
			state.isLoading = false
            state.message = action.payload;
		}
    },
    extraReducers: (builder) =>
        builder
            .addCase(authActions.login.fulfilled, (state, action) => {
                const response = action.payload;

				state.isSuccess = true,
				state.isError = false,
				state.isLoading = false,
                state.message = response.message;
            })
            .addCase(authActions.login.pending, (state) => {
                state.isSuccess = false,
				state.isError = false,
				state.isLoading = true
            })
            .addCase(authActions.getCurrentUser.fulfilled, (state, action) => {
                const response = action.payload

                state.isSuccess = true,
				state.isError = false,
				state.isLoading = false,
                state.message = response.message
            })
            .addCase(authActions.getCurrentUser.pending, (state) => {
                state.isSuccess = false,
				state.isError = false,
				state.isLoading = true,
                state.message = ''
            })
});

export default StatusSlice.reducer;
export const { handleErrorRequest, handleResetStatus, handleSuccessStatus } = StatusSlice.actions;
