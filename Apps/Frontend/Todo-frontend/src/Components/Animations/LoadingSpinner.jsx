import { motion } from "framer-motion";

function LoadingSpinner () {
  return (
    <div className="absolute top-8 left-1/2 -translate-x-1/2 bg-white w-10 h-10 flex justify-center items-center rounded-full shadow-[0_0_15px_rgba(0,0,0,0.5)]">
      <motion.div
        className="w-8 h-8 border-4 border-gray-300 border-t-black rounded-full"
        animate={{ rotate: 360}}
        transition={{
          repeat: Infinity,
          duration: 1,
          ease: "linear",
        }}
      />
    </div>
  );
};

export default LoadingSpinner;