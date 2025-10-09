import { useFormik } from "formik";
import { useNavigate } from "react-router-dom";

import * as Yup from "yup";
import { useDispatch } from "react-redux";
import { authActions } from "../../Redux/Actions/AuthActions";
import Button from "../Inputs/Button";

function RegisterForm() {
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const formik = useFormik({
        initialValues: {
            identify: "",
            password: "",
            name: "",
        },
        validationSchema: Yup.object({
            name: Yup.string().required("Tên người dùng không được để trống"),
            identify: Yup.string()
                .email("Email không xác đinh")
                .required("Tài khoản không được để trống"),
            password: Yup.string()
                .min(1, "Mật khẩu phải dài hơn 1 ký tự")
                .required("Mật khẩu không được để trống"),
        }),
        onSubmit: async (request) => {
            try {
                var registerAction = await dispatch(authActions.register(request));
                if (authActions.register.fulfilled.match(registerAction)) {
                    navigate("/login");
                } else {
                    console.error("Register failed:", authActions.register.payload);
                }
            } catch (error) {
                console.error(error);
            }
        },
    });

    return (
        <form
            className="w-2/3 flex flex-col gap-5 px-5 py-16 rounded-[10px] shadow-[0_0_20px_rgba(0,0,0,0.5)]"
            onSubmit={formik.handleSubmit}
        >
            <h1 className="text-xl text-center font-bold">REGISTER</h1>
			<div className="mb-5">
                <div className="flex justify-between gap-2 text-4xl">
                    <div className="w-[4rem] h-[4rem] flex justify-center items-center">
                        <i className="fas fa-user"></i>
                    </div>
                    <input
                        className="flex-1 outline-0"
                        type="text"
                        value={formik.values.name}
                        onChange={formik.handleChange}
                        name="name"
                        onBlur={formik.handleBlur}
                        placeholder="Tên người dùng"
                    ></input>
                </div>
                <hr className=" text-2xl"></hr>
                {formik.errors.name && formik.touched.name && (
                    <p className="text-red-500 text-2xl mt-1 [text-shadow:0px_0px_5px_black]">
                        * {formik.errors.name}
                    </p>
                )}
            </div>
            <div className="mb-5">
                <div className="flex justify-between gap-2 text-4xl">
                    <div className="w-[4rem] h-[4rem] flex justify-center items-center">
                        <i className="fas fa-user"></i>
                    </div>
                    <input
                        className="flex-1 outline-0"
                        type="text"
                        value={formik.values.identify}
                        onChange={formik.handleChange}
                        name="identify"
                        onBlur={formik.handleBlur}
                        placeholder="Tên đăng nhập..."
                    ></input>
                </div>
                <hr className=" text-2xl"></hr>
                {formik.errors.identify && formik.touched.identify && (
                    <p className="text-red-500 text-2xl mt-1 [text-shadow:0px_0px_5px_black]">
                        * {formik.errors.identify}
                    </p>
                )}
            </div>

            <div>
                <div className="flex justify-between gap-2 text-4xl">
                    <div className="w-[4rem] h-[4rem] flex justify-center items-center">
                        <i className="fas fa-key"></i>
                    </div>
                    <input
                        className="flex-1 outline-0"
                        type="password"
                        value={formik.values.password}
                        onChange={formik.handleChange}
                        name="password"
                        placeholder="******"
                        onBlur={formik.handleBlur}
                    ></input>
                </div>
                <hr className=" text-2xl"></hr>
                {formik.errors.password && formik.touched.password && (
                    <p className="text-red-500 text-2xl mt-1 [text-shadow:0px_0px_5px_black]">
                        * {formik.errors.password}
                    </p>
                )}
            </div>

            <Button label="Đăng ký" type="submit"/>
            <Button label="Đã có tài khoản" type="button" action={() => navigate("/login")}/>
        </form>
    );
}

export default RegisterForm;
