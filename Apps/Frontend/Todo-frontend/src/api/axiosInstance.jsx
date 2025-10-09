import axios from "axios";
import { logout } from "../Redux/Slices/AuthSlice";
import {
    handleErrorRequest,
    handleSuccessStatus,
} from "../Redux/Slices/StatusSlice";

const axiosInstance = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    withCredentials: true,
    headers: {
        "Content-Type": "application/json",
    },
});

let store;

export const injectStore = (_store) => {
    store = _store;
};

axiosInstance.interceptors.response.use(
    (response) => {
        if (response.config.showSuccess && store) {
            store.dispatch(handleSuccessStatus(response.data.message));
        }
        return response;
    },
    async (error) => {
        if (error.response?.status === 401) {
            try {
                await axios.post(
                    `${import.meta.env.VITE_API_URL}/api/Auth/refresh`,
                    {},
                    { withCredentials: true }
                );

                return axiosInstance.request(error.config);
            } catch (er) {
                if (store) {
                    store.dispatch(logout());
                }
                return Promise.reject(er);
            }
        }

        const responseError = error.response?.data || {
            message: "Unexpected error",
        };
        store.dispatch(handleErrorRequest(responseError.message));
        return Promise.reject(error);
    }
);

export default axiosInstance;
