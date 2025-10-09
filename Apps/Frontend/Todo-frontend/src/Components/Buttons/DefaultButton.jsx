function DefaultButton({ label, action, className }) {
    return (
        <button
            className={`border hover:cursor-pointer hover:shadow-[0_0_20px_rgba(0,0,0,0.5)] ${className}`}
            onClick={action}
        >
            {label}
        </button>
    );
}

export default DefaultButton;
