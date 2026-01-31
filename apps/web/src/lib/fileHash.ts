import { createBLAKE3 } from "hash-wasm";

export async function hashFileStream(file: File): Promise<string> {
	const hasher = await createBLAKE3();
	const reader = file.stream().getReader();

	while (true) {
		const { done, value } = await reader.read();
		if (done) break;
		hasher.update(value);
	}

	return hasher.digest("hex");
}

export async function hashBlobStream(blob: Blob): Promise<string> {
	const hasher = await createBLAKE3();
	const reader = blob.stream().getReader();
	while (true) {
		const { done, value } = await reader.read();
		if (done) break;
		hasher.update(value);
	}
	return hasher.digest("hex");
}
