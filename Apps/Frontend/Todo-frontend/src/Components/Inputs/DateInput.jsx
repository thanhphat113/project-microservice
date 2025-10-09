function DateInput({ value, setValue, className, min }) {
    return (
        <input
            className={`border p-2 bg-white ${className}`}
            type="date"
            value={value}
			min = {min}
            onChange={(e) => setValue(e.target.value)}
        />
    );
}

export default DateInput;
