import { useFormik } from "formik";

import * as Yup from "yup";
import { taskService } from "../../api/task/TaskService";
import Button from "../Inputs/Button";
import DialogLayout from "../Layouts/DialogLayout";
import DateInput from "../Inputs/DateInput";

function UpdateTaskForm({ task, minDate, setTargetTaskUpdate, setData }) {
    const formik = useFormik({
        initialValues: {
            taskId: task.taskId,
            name: task.name,
            expiration: task.expiration,
        },
        validationSchema: Yup.object({
            name: Yup.string().required("Tên không được để trống"),
            expiration: Yup.date()
                .required("Ngày hết hạn không được để trống")
                .typeError("Ngày hết hạn không hợp lệ"),
        }),
        onSubmit: async (values) => {
            var response = await taskService.updateTask(values);
            if (response) {
                var data = response.data;
                setData((prevData) =>
                    prevData.map((task) =>
                        task.taskId === data.taskId ? data : task
                    )
                );
            }
        },
    });

    console.log(task);

    return (
        <DialogLayout className=" bg-[rgba(0,0,0,0.3)]">
            <form onSubmit={formik.handleSubmit}>
                <h1 className="text-xl text-center font-bold">UPDATE</h1>
                <div className="mb-5">
                    <div className="flex justify-between gap-2 text-4xl">
                        <div className="w-[4rem] h-[4rem] flex justify-center items-center">
                            <i className="fas fa-user"></i>
                        </div>
                        <input
                            className="flex-1 outline-0"
                            type="text"
                            value={formik.values.taskId}
                            onChange={formik.handleChange}
                            name="taskId"
                            onBlur={formik.handleBlur}
                            readOnly
                        ></input>
                    </div>
                    <hr className=" text-2xl"></hr>
                </div>
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

                <div>
                    <div className="flex text-4xl">
                        <DateInput
                            value={formik.values.expiration}
                            setValue={(val) =>
                                formik.setFieldValue("expiration", val)
                            }
                            min={minDate}
                            className="w-full"
                        />
                    </div>
                    {formik.errors.password && formik.touched.password && (
                        <p className="text-red-500 text-2xl mt-1 [text-shadow:0px_0px_5px_black]">
                            * {formik.errors.password}
                        </p>
                    )}
                </div>

                <div className="mt-2 flex gap-3 justify-center">
                    {formik.dirty && (
                        <Button
                            className="bg-green-400 text-white border-black"
                            label="Update"
                            type="submit"
                        />
                    )}
                    <Button
                        label="Cancel"
                        type="button"
                        action={() => setTargetTaskUpdate()}
                    />
                </div>
            </form>
        </DialogLayout>
    );
}

export default UpdateTaskForm;
