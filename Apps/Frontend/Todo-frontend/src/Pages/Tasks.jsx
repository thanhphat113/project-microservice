import { useSelector } from "react-redux";
import TaskTable from "../Components/Tables/TaskTable";
import Text from "../Components/Inputs/Text";
import { useEffect, useState } from "react";
import Button from "../Components/Inputs/Button";
import DateInput from "../Components/Inputs/DateInput";
import { taskService } from "../api/task/TaskService";
import DialogLayout from "../Components/Layouts/DialogLayout";
import UpdateTaskForm from "../Components/Forms/UpdateTaskForm";

function Tasks() {
    const [name, setName] = useState("");
    const [expiration, setExpiration] = useState("");
    const [data, setData] = useState([]);

    const [targetTaskUpdate, setTargetTaskUpdate] = useState();

    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    const minDate = tomorrow.toISOString().split("T")[0];

    const { information } = useSelector((state) => state.auth);
    const headers = ["Task Id", "Name", "Expiration"];

    useEffect(() => {
        const fetchAsync = async () => {
            const response = await taskService.getTasksByUserId();
            setData(response.data);
        };
        fetchAsync();
    }, []);

    useEffect(() => {
        console.log(data);
    }, [data]);

    const handleAddTask = async (values) => {
        const response = await taskService.addTask(values);
        if (response) {
            setData((prevData) => [...prevData, response.data]);
            setName("");
            setExpiration("")
        }
    };

    return (
        <>
            {targetTaskUpdate && (
                <UpdateTaskForm
                    task={targetTaskUpdate}
                    minDate={minDate}
                    setTargetTaskUpdate={setTargetTaskUpdate}
                    setData={setData}
                ></UpdateTaskForm>
            )}
            <div className=" w-full h-full p-5 flex-col flex justify-center items-center">
                <div className="w-2/4 flex mb-2 gap-2">
                    <Text
                        value={name}
                        setValue={setName}
                        className="w-full"
                    ></Text>
                    <DateInput
                        value={expiration}
                        setValue={setExpiration}
                        min={minDate}
                        className="w-1/4"
                    />
                    <Button
                        type="button"
                        label="Add"
                        action={() => handleAddTask({ name, expiration })}
                        className="hover:bg-green-400"
                    />
                </div>
                <TaskTable
                    headers={headers}
                    data={data}
                    setData={setData}
                    setTargetTaskUpdate={setTargetTaskUpdate}
                />
            </div>
        </>
    );
}

export default Tasks;
