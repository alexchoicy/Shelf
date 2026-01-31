export async function cropTo16x9(file: File, quality = 0.9): Promise<Blob> {
	return new Promise((resolve, reject) => {
		const img = new Image();
		const url = URL.createObjectURL(file);

		img.onload = () => {
			URL.revokeObjectURL(url);

			const canvas = document.createElement("canvas");
			const ctx = canvas.getContext("2d");

			if (!ctx) {
				reject(new Error("Failed to get canvas context"));
				return;
			}

			const targetRatio = 16 / 9;
			const imgRatio = img.width / img.height;

			let srcX = 0;
			let srcY = 0;
			let srcWidth = img.width;
			let srcHeight = img.height;

			if (imgRatio > targetRatio) {
				srcWidth = img.height * targetRatio;
				srcX = (img.width - srcWidth) / 2;
			} else if (imgRatio < targetRatio) {
				srcHeight = img.width / targetRatio;
				srcY = (img.height - srcHeight) / 2;
			}

			canvas.width = 1920;
			canvas.height = 1080;

			ctx.drawImage(
				img,
				srcX,
				srcY,
				srcWidth,
				srcHeight,
				0,
				0,
				canvas.width,
				canvas.height,
			);

			canvas.toBlob(
				(blob) => {
					if (blob) {
						resolve(blob);
					} else {
						reject(new Error("Failed to create blob"));
					}
				},
				"image/jpeg",
				quality,
			);
		};

		img.onerror = () => {
			URL.revokeObjectURL(url);
			reject(new Error("Failed to load image"));
		};

		img.src = url;
	});
}
