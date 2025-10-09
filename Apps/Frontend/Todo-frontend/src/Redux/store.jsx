import { configureStore } from "@reduxjs/toolkit";
import statusReducer from "./Slices/StatusSlice";
import authReducer from "./Slices/AuthSlice";
import { injectStore } from "../api/axiosInstance";

const store = configureStore({
  reducer: {
    status: statusReducer,
    auth: authReducer
  },
});

injectStore(store)

export default store