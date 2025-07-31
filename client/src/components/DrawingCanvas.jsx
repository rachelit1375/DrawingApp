import React, { useRef, useEffect } from "react";
import "../css/DrawingCanvas.css";

function DrawingCanvas({ commands }) {
  const canvasRef = useRef(null);

  useEffect(() => {
    const canvas = canvasRef.current;
    const ctx = canvas.getContext("2d");
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    commands.forEach((cmd) => {
      switch (cmd.type) {
        case "circle":
          ctx.beginPath();
          ctx.arc(cmd.x, cmd.y, cmd.radius, 0, Math.PI * 2);
          ctx.fillStyle = cmd.color || "black";
          ctx.fill();
          break;
        case "rect":
          ctx.fillStyle = cmd.color || "black";
          ctx.fillRect(cmd.x, cmd.y, cmd.width, cmd.height);
          break;
        case "line":
          ctx.beginPath();
          ctx.moveTo(cmd.x1, cmd.y1);
          ctx.lineTo(cmd.x2, cmd.y2);
          ctx.strokeStyle = cmd.color || "black";
          ctx.lineWidth = cmd.lineWidth || 2;
          ctx.stroke();
          break;
        // אפשר להוסיף עוד סוגים: ellipse, triangle וכו'
        default:
          break;
      }
    });
  }, [commands]);

  return (
    <div className="canvas-wrapper">
      <canvas ref={canvasRef} width={800} height={400} className="drawing-canvas" />
    </div>
  );
}

export default DrawingCanvas;
