'use server';

import { DEGREE_SCHEMA } from '@/entities';
import { getBaseApiUrl, getZodErrorsString, logError } from '@/utils';
import { badRequest, ok, unauthorized } from '@/utils/server';
import { cookies } from 'next/headers';
import { z } from 'zod';

const GET_ALL_DEGREES_SUCCESS_RESPONSE_SCHEMA = z.object({
  data: z.array(DEGREE_SCHEMA.and(z.object({
    completedCredits: z.number(),
    totalCredits: z.number(),
  }))),
  error: z.null(),
});

export const getAllDegrees = async () => {
  const cookieStore = await cookies();
  const accessToken = cookieStore.get('accessToken')?.value;

  if (!accessToken) return unauthorized();

  const endpoint = `${getBaseApiUrl()}/degrees`;

  return await fetch(endpoint, {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  }).then(async (res) => {
    if (res.status === 401) return unauthorized();

    const data = await res.json();
    const parsedData = GET_ALL_DEGREES_SUCCESS_RESPONSE_SCHEMA.safeParse(data);

    if (!parsedData.success) return badRequest(getZodErrorsString(parsedData.error));

    return ok(parsedData.data);
  }).catch((error) => {
    logError(error);

    return badRequest('An error occurred while fetching degrees');
  });
};