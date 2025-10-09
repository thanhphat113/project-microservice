import { taskService } from "../../api/task/TaskService";

function TaskTable({ headers, data, setData, setTargetTaskUpdate}) {
    const handleDeleteTask = async (id) => {
        const response = await taskService.deleteTask(id);
        if (response)
            setData((prevData) =>
                prevData.filter((task) => task.taskId !== id)
            );
    };

    const handleUpdate = (id) =>{
        const item = data.find(item => item.taskId === id)
        setTargetTaskUpdate(item)
    }

    return (
        <table className="min-w-full border border-gray-300 border-collapse text-sm text-left">
            <thead className="bg-gray-100">
                <tr>
                    {headers.map((h, i) => (
                        <th key={i} className="border px-4 py-2">
                            {h}
                        </th>
                    ))}
                    <th className="border px-4 py-2">Action</th>
                </tr>
            </thead>
            <tbody>
                {data &&
                    data.map((item) => (
                        <tr key={item.taskId} className="hover:bg-gray-50">
                            <td className="border px-4 py-2">{item.taskId}</td>
                            <td className="border px-4 py-2">{item.name}</td>
                            <td className="border px-4 py-2">
                                {item.expiration}
                            </td>
                            <td className="border px-4 py-2 flex justify-center gap-5">
                                <i onClick={() => handleUpdate(item.taskId)} className="text-xl fa-solid fa-hammer p-[0.5rem] rounded-xl cursor-pointer hover:text-white bg-green-400 hover:bg-green-500 hover:scale-[1.1] hover:shadow-[0_0_15px_rgba(0,0,0,0.5)]" />
                                <i
                                    onClick={() =>
                                        handleDeleteTask(item.taskId)
                                    }
                                    className="text-xl fa-solid fa-trash p-[0.5rem] rounded-xl cursor-pointer hover:text-white bg-red-400 hover:bg-red-500 hover:scale-[1.1] hover:shadow-[0_0_15px_rgba(0,0,0,0.5)]"
                                />
                            </td>
                        </tr>
                    ))}
            </tbody>
        </table>
    );
}

export default TaskTable;
