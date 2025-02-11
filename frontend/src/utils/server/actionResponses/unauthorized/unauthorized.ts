import { redirect } from 'next/navigation';

export const unauthorized = () => redirect('/login');
