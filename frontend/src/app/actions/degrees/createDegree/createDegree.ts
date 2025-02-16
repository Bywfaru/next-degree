'use server';

import { DEGREE_SCHEMA, STATUSES } from '@/entities';
import { getBaseApiUrl, getZodErrorsString, logError } from '@/utils';
import { badRequest, ok, unauthorized } from '@/utils/server';
import { cookies } from 'next/headers';
import { z } from 'zod';

const CREATE_DEGREE_SUCCESS_RESPONSE_SCHEMA = z.object({
  data: DEGREE_SCHEMA,
  error: z.null(),
});

const CREATE_DEGREE_SCHEMA = z.object({
  name: z.string().nonempty(),
  status: z
    .preprocess(
      (value) => STATUSES.findIndex((status) => status === value),
      z.number().int().optional(),
    )
    .optional(),
});

type CreateDegreeSchema = z.infer<typeof CREATE_DEGREE_SCHEMA>;

export const createDegree = async (data: CreateDegreeSchema) => {
  const cookieStore = await cookies();
  const accessToken = cookieStore.get('accessToken')?.value;

  if (!accessToken) return unauthorized();

  const parsedData = CREATE_DEGREE_SCHEMA.safeParse(data);

  if (!parsedData.success)
    return badRequest(getZodErrorsString(parsedData.error));

  const endpoint = `${getBaseApiUrl()}/degrees`;

  return await fetch(endpoint, {
    method: 'POST',
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
    body: JSON.stringify(data),
  })
    .then(async (res) => {
      if (res.status === 401) return unauthorized();

      const data = await res.json();
      const parsedData = CREATE_DEGREE_SUCCESS_RESPONSE_SCHEMA.safeParse(data);

      if (!parsedData.success)
        return badRequest(getZodErrorsString(parsedData.error));

      return ok(parsedData.data);
    })
    .catch((error) => {
      logError(error);

      return badRequest('An error occurred while creating the degree');
    });
};
