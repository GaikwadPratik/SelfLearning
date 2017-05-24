import sys, binascii, re
from subprocess import Popen, PIPE

f=open(sys.argv[1], 'r')
for line in f:
	wepKey = re.sub(r'\W+', '', line)

	if len(wepKey) !=5 :
		continue
	hexKey = binascii.hexlify(wepKey)

	print "Trying with WEP Key: " +wepKey + "Hex: " + hexKey

	p = Popen(['airdecap-ng', '-w', hexKey, 'wificracking1_1'], stdout=PIPE)
	output=p.stdout.read()

	finalResult = output.split('\n')[4]
	if finalResult.find('1') !=-1 :
		print "Success Wep Key Found: " + wepKey
		sys.exit(0)

print "Failure! Wep key could not be found in this dictionaty!" 

