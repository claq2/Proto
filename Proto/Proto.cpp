#include <string>
#include <iostream>
#include <fstream>
#include <SimpleAmqpClient/SimpleAmqpClient.h>
#include "JobConfig.pb.h"
using namespace std;

int main()
{
	boost::shared_ptr<int> myint;
	AmqpClient::Channel::ptr_t connection = AmqpClient::Channel::Create("localhost");
	GetJobConfigRequest request;
	fstream input("..\\request.bin", ios::in | ios::binary);
	if (!input)
	{
		cout << "..\\request.bin not found." << endl;
		return -1;
	}
	else if (!request.ParseFromIstream(&input))
	{
		cerr << "Failed to parse request." << endl;
		return -1;
	}

	std::cout << "Job ID " << request.jobid() << " User ID " << request.userid() << std::endl;
}
