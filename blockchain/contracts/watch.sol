// SPDX-License-Identifier: MIT
pragma solidity >=0.7.0;
pragma experimental ABIEncoderV2;


contract WatchContract {
    struct Watch {
        address vendorAddress;
        address clientAddress;
        uint128 GUID;
        uint    materialsReceivedTimeStamp;
        string  watchType;
        string  creationAddress;
        string  shippingAddress;
        string  deliveryAddress;
        uint    assembledTimeStamp;
        uint    sentTimeStamp;
        uint    deliveredTimeStamp;
    }

    mapping(uint128 => Watch) watches;

    function createWatch(address vendorAddress, uint128 GUID, string memory watchType) public {
        require(watches[GUID].GUID != GUID);
        watches[GUID].vendorAddress = vendorAddress;
        watches[GUID].clientAddress = msg.sender;
        watches[GUID].GUID = GUID;
        watches[GUID].watchType = watchType;
    }

    function getWatch(address vendorAddress, uint128 GUID) private view returns (Watch storage) {
        require(GUID != 0);
        Watch storage returnWatch = watches[GUID];
        require(vendorAddress == returnWatch.vendorAddress);
        require(returnWatch.clientAddress == msg.sender);
        return returnWatch;
    }

    function materialsReceived(address vendorAddress, uint128 GUID) public {
        Watch storage watch = getWatch(vendorAddress, GUID);
        watch.materialsReceivedTimeStamp = block.timestamp;
        watches[watch.GUID] = watch;
    }

    function WatchAssembled(address vendorAddress, string memory physicalAddress, uint128 WatchGUID) public {
        Watch storage watch = getWatch(vendorAddress, WatchGUID);
        watch.assembledTimeStamp = block.timestamp;
        watch.creationAddress = physicalAddress;
        watches[watch.GUID] = watch;
    }

    function WatchSent(address vendorAddress,  uint128 WatchGUID, string memory sendingAddress, string memory receivingAddress) public {
        Watch storage watch = getWatch(vendorAddress, WatchGUID);
        require(StringsAreEqual(watch.creationAddress, sendingAddress));
        watch.sentTimeStamp = block.timestamp;
        watch.shippingAddress = sendingAddress;
        watch.deliveryAddress = receivingAddress;
        watches[watch.GUID] = watch;
    }

    function WatchReceived(address vendorAddress, uint128 WatchGUID) public {
        Watch storage watch = getWatch(vendorAddress, WatchGUID);
        watch.deliveredTimeStamp = block.timestamp;
        watches[watch.GUID] = watch;
    }

    function StringsAreEqual(string memory str, string memory other) public pure returns (bool) {
        return (keccak256(abi.encodePacked((str))) == keccak256(abi.encodePacked((other))));
    }

    function GetVendorAddress(uint128 GUID) public view returns (address) {
        return watches[GUID].vendorAddress;
    }

    function GetClientAddress(uint128 GUID) public view returns (address) {
        return watches[GUID].clientAddress;
    }

    function GetGUID(uint128 GUID) public view returns (uint128) {
        return watches[GUID].GUID;
    }

    function GetMaterialsReceivedTimeStamp(uint128 GUID) public view returns (uint) {
        return watches[GUID].materialsReceivedTimeStamp;
    }

    function GetWatchType(uint128 GUID) public view returns (string memory) {
        return watches[GUID].watchType;
    }

    function GetCreationAddress(uint128 GUID) public view returns (string memory) {
        return watches[GUID].creationAddress;
    }

    function GetShippingAddress(uint128 GUID) public view returns (string memory) {
        return watches[GUID].shippingAddress;
    }

    function GetDeliveryAddress(uint128 GUID) public view returns (string memory) {
        return watches[GUID].deliveryAddress;
    }

    function GetAssembledTimeStamp(uint128 GUID) public view returns (uint) {
        return watches[GUID].assembledTimeStamp;
    }

    function GetSentTimeStamp(uint128 GUID) public view returns (uint) {
        return watches[GUID].sentTimeStamp;
    }

    function GetDeliveredTimeStamp(uint128 GUID) public view returns (uint) {
        return watches[GUID].deliveredTimeStamp;
    }

}
