'use server';

import { unauthorized } from '@/utils/server';
import { cookies } from 'next/headers';
import type { FC, PropsWithChildren } from 'react';

const DashboardLayout: FC<Required<PropsWithChildren>> = async ({ children }) => {
  const cookieStore = await cookies();

  if (!cookieStore.has('accessToken')) unauthorized();

  return children;
};

export default DashboardLayout;
