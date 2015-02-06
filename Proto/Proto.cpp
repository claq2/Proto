#include <string>
#include <iostream>
#include <vector>
#include <boost/shared_ptr.hpp>
#include <SimpleAmqpClient/SimpleAmqpClient.h>

#include "JobConfig.pb.h"

using namespace std;
using namespace AmqpClient;

int main()
{
	GetJobConfigRequest request;
	string body;
	try
	{
		AmqpClient::Channel::ptr_t connection = AmqpClient::Channel::Create("10.51.3.43", 5672, "evault", "xxxxxx");
		connection->DeclareQueue("jamesqueue", false, true, false);
		connection->BindQueue("jamesqueue", "evault.to");
		connection->BasicConsume("jamesqueue", "jamesqueue");
		cout << "Waiting for message..." << endl;
		Envelope::ptr_t envelope = connection->BasicConsumeMessage();
		body = envelope->Message()->Body();
	}
	catch (exception e)
	{
		cout << e.what() << endl;
	}

	if (!request.ParseFromString(body))
	{
		cerr << "Failed to parse request." << endl;
		return -1;
	}

	std::cout << "Job ID " << request.jobid() << " User ID " << request.userid() << std::endl;
}

