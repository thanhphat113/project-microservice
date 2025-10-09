function Button({ className, label, type, action }) {
    return (
        <input
            type={type}
            className={`border hover:cursor-pointer hover:shadow-[0_0_20px_rgba(0,0,0,0.5)] px-2 py-2 rounded-[10px] ${className}`}
            value={label}
            onClick={action}
        ></input>
    );
}

export default Button;
