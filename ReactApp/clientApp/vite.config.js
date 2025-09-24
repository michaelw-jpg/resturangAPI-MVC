import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

export default defineConfig({
    plugins: [react()],
    define: {
        'process.env.NODE_ENV': JSON.stringify('production'), // <--- fixes process is not defined
    },
    build: {
        lib: {
            entry: path.resolve(__dirname, 'src/main.jsx'),
            name: 'mountBookingForm',
            fileName: () => 'bookingForm.bundle.js',
            formats: ['iife'],
        },
        outDir: path.resolve(__dirname, '../../wwwroot/js'),
        emptyOutDir: true
    }
});
