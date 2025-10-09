import ZoomAnimation from "../Animations/ZoomAnimation";

function DialogLayout({className, children}) {
	return ( 
		<div className={`absolute w-full h-full top-0 left-0 flex justify-center items-center ${className}`}>
			<ZoomAnimation>{children}</ZoomAnimation>
		</div>
	 );
}

export default DialogLayout;