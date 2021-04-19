// SPDX-License-Identifier: MIT
pragma solidity >=0.4.22;



contract WatchContract {
    struct Watch {
    
        address vendorAddress;
        address clientAddress;
        uint64  GUID;
        uint    materialsRecievedTimeStamp;
        string  watchType;
        string  creationAddress;
        string  shippingAddress;
        string  deliveryAddress;
        uint    assembledTimeStamp;
        uint    sentTimeStamp;
        uint    deliveredTimeStamp;
    }

    mapping(uint64 => Watch) watches;

    function createWatch(address vendorAddress, uint64 GUID, string memory watchType) public {
        require(watches[GUID].GUID != GUID);
        watches[GUID].vendorAddress = vendorAddress;
        watches[GUID].clientAddress = msg.sender;
        watches[GUID].GUID = GUID;
        watches[GUID].watchType = watchType;
    }

    function getWatch(address vendorAddress, uint64 GUID) private view returns (Watch storage) {
        require(GUID != 0);
        Watch storage returnWatch = watches[GUID];
        require(vendorAddress == returnWatch.vendorAddress);
        require(returnWatch.clientAddress == msg.sender);
        return returnWatch;
    }

    function materialsRecieved(address vendorAddress, uint64 GUID) public {
        Watch storage watch = getWatch(vendorAddress, GUID);
        watch.materialsRecievedTimeStamp = block.timestamp;
        watches[watch.GUID] = watch;
    }

    function WatchAssembled(address vendorAddress, string memory physicalAddress, uint64 WatchGUID) public {
        Watch storage watch = getWatch(vendorAddress, WatchGUID);
        watch.assembledTimeStamp = block.timestamp;
        watch.creationAddress = physicalAddress;
        watches[watch.GUID] = watch;
    }

    function WatchSent(address vendorAddress,  uint64 WatchGUID, string memory sendingAddress, string memory recievingAddress) public {
        Watch storage watch = getWatch(vendorAddress, WatchGUID);
        require(StringsAreEqual(watch.creationAddress, sendingAddress));
        watch.sentTimeStamp = block.timestamp;
        watch.shippingAddress = sendingAddress;
        watch.deliveryAddress = recievingAddress;
        watches[watch.GUID] = watch;
    }

    function WatchRecieved(address vendorAddress, uint64 WatchGUID) public {
        Watch storage watch = getWatch(vendorAddress, WatchGUID);
        watch.deliveredTimeStamp = block.timestamp;
        watches[watch.GUID] = watch;
    }

    function StringsAreEqual(string memory str, string memory other) public pure returns (bool) {
        return (keccak256(abi.encodePacked((str))) == keccak256(abi.encodePacked((other))));
    }
}
