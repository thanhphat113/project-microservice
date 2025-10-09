import { useDispatch } from "react-redux";
import DialogLayout from "../Layouts/DialogLayout";
import { handleResetStatus } from "../../Redux/Slices/StatusSlice";
import LoadingSpinner from "../Animations/LoadingSpinner";
import DefaultButton from "../Buttons/DefaultButton";

function GlobalNotification({message, isSuccess, isError, isLoading}) {
    const dispatch = useDispatch();

	if (isLoading) return <LoadingSpinner/>

    return (
        <DialogLayout className="bg-[rgba(0,0,0,0.3)] z-10">
            <div className="bg-white w-[50rem] py-[2rem] relative rounded-3xl">
                <h1 className="text-center text-5xl font-bold">Notification</h1>
                <i
                    onClick={() => dispatch(handleResetStatus())}
                    className="fas fa-times absolute right-3 top-3 text-3xl hover:text-red-700 cursor-pointer"
                ></i>
                <div className="text-center mt-5 text-3xl flex justify-center items-center gap-5">
                    {isSuccess && <i className="far fa-check-circle text-4xl" style={{color: "#63E6BE"}}></i>}
                    {isError && <div className="p-1 border w-[3.5rem] h-[3.5rem] rounded-[50%] text-red-500 text-4xl"><i className="fas fa-exclamation"></i></div>}
                {message}
                </div>
                <div className="flex justify-center mt-8 h-[3.2rem]">
                    <DefaultButton
                        label="Confirm"
                        action={() => dispatch(handleResetStatus())}
                        className={`px-4 hover:bg-green-300 h-[2.5rem] hover:text-white border-black rounded-[10px]`}
                    />
                </div>
            </div>
        </DialogLayout>
    );
}

export default GlobalNotification;
