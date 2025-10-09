function Text({value, setValue, className}) {
	return ( 
		<input className={`border p-2 bg-white ${className}`} type="text" value={value} onChange={(e) => setValue(e.target.value)}/>
	 );
}

export default Text;