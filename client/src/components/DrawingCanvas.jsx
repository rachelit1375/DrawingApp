import React, { useRef, useEffect } from "react";
import "../css/DrawingCanvas.css";

function DrawingCanvas({ commands }) {
  const canvasRef = useRef(null);

  useEffect(() => {
    const canvas = canvasRef.current;
    const ctx = canvas.getContext("2d");
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    if (!ctx) {
      console.error("No canvas context!");
      return;
    }

    console.log("commandsInCanvas ");
    console.log(commands);


    commands.forEach((cmd) => {
      const shape = cmd.shape?.toLowerCase(); // Normalize shape string

      switch (shape) {
        case "rectangle":
          ctx.fillStyle = cmd.color || "black";
          ctx.fillRect(cmd.x, cmd.y, cmd.width, cmd.height);
          break;

        case "circle":
          ctx.beginPath();
          ctx.arc(cmd.x, cmd.y, cmd.radius || 20, 0, Math.PI * 2);
          ctx.fillStyle = cmd.color || "black";
          ctx.fill();
          break;

        case "line":
          if (cmd.from && cmd.to) {
            ctx.beginPath();
            ctx.moveTo(cmd.from[0], cmd.from[1]);
            ctx.lineTo(cmd.to[0], cmd.to[1]);
            ctx.strokeStyle = cmd.color || "black";
            ctx.lineWidth = 2;
            console.log(cmd.color)
            ctx.stroke();
          } else {
            console.warn("Line command missing 'from' or 'to':", cmd);
          }
          break;

        case "triangle": {
          const topX = cmd.x;
          const topY = cmd.y;
          const width = cmd.width || 50;
          const height = cmd.height || 50;

          const leftX = topX - width / 2;
          const leftY = topY + height;
          const rightX = topX + width / 2;
          const rightY = topY + height;

          ctx.beginPath();
          ctx.moveTo(topX, topY);
          ctx.lineTo(leftX, leftY);
          ctx.lineTo(rightX, rightY);
          ctx.closePath();
          ctx.fillStyle = cmd.color || "black";
          ctx.fill();
          break;
        }

        default:
          console.warn("Unknown shape:", cmd.shape);
          break;
      }
    });
  }, [commands]);

  return (
    <div className="canvas-wrapper">
      <canvas
        ref={canvasRef}
        width={800}
        height={400}
        className="drawing-canvas"
      />
    </div>
  );
}

export default DrawingCanvas;
