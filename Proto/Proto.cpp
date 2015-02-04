#include <string>
#include <iostream>
#include "JobConfig.pb.h"

int main()
{
	GetJobConfigRequest request;
	request.set_jobid("job:123");
	std::cout << "Job ID " << request.jobid() << std::endl;
}
