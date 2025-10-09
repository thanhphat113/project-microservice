import { motion } from "framer-motion";

function ZoomAnimation ({ children }) {
	return (
	  <motion.div
		initial={{ scale: 0.1, opacity: 0 }} 
		animate={{ scale: 1, opacity: 1 }}
		exit={{scale: 0.1, opacity:0}}  
		transition={{ duration: 0.2, ease: "easeOut" }} 
		className="p-1 bg-white shadow-lg rounded-2xl"
	  >
		{children}
	  </motion.div>
	);
  };

export default ZoomAnimation;

  