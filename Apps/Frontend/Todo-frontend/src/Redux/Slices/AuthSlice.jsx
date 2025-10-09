import { createSlice } from "@reduxjs/toolkit";
import { authActions } from "../Actions/AuthActions";

const AuthSlice = createSlice({
    name: "auth",
    initialState: {
        information: null,
        isAuthenticated: false,
    },
    reducers: {
        logout: (state) => {
            state.user = null;
            state.isAuthenticated = false;
        },
    },
    extraReducers: (builder) =>
        builder
            .addCase(authActions.login.fulfilled, (state, action) => {
                const response = action.payload;
                state.information = response.data;
                state.isAuthenticated = true;
            })
            .addCase(authActions.login.pending, (state) => {
                state.information = null;
                state.isAuthenticated = false;

            })
            .addCase(authActions.login.rejected, (state) => {
                state.information = null;
                state.isAuthenticated = false;
            })
            .addCase(authActions.getCurrentUser.fulfilled, (state, action) => {
                var response = action.payload;
                state.information = response.data;
                state.isAuthenticated = true
            })
            .addCase(authActions.getCurrentUser.pending, (state) => {
                state.information = null;
                state.isAuthenticated = false;

            })
            .addCase(authActions.getCurrentUser.rejected, (state) => {
                state.information = null;
                state.isAuthenticated = false;
            })
});

export default AuthSlice.reducer;
export const { logout } = AuthSlice.actions;
